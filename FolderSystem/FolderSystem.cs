using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
namespace FolderSystem
{
   

    public class FolderSystem
    {
        
        public static List<string> ShowStructure(List<string> paths)
        {
            paths.Sort();
            int counter = 1;
            string substring = "";
            int lastIndexOfSlash = 0;
            Dictionary<string, int> occurences = new Dictionary<string, int>();

            List<string> res = new List<string>();
            foreach(string path in paths)
            {
                if (counter == 1)
                {
                    Console.WriteLine(path);
                    res.Add(path);
                    counter++;
                }
                else
                {
                    lastIndexOfSlash = path.LastIndexOf('\\');
                    if(!path.Substring(lastIndexOfSlash,path.Length-lastIndexOfSlash).Contains('.'))
                    {
                        string s = string.Format("{0} {1}", new string('+', (HowMany(path, '\\')) * 2), path.Substring(lastIndexOfSlash, path.Length - lastIndexOfSlash));

                        if (!occurences.ContainsKey(path.Substring(0, lastIndexOfSlash)))
                        {
                            res.Add(s);
                            occurences.Add(path, 1);
                            Console.WriteLine(s);
                        }
                        else
                        {
                            res.Add(s);
                            Console.WriteLine(s);
                        }
                    }
                    
                    else
                    {
                        substring = path.Substring(lastIndexOfSlash, path.Length - lastIndexOfSlash );
                        res.Add(string.Format("{0} {1}", new string('.', (HowMany(path, '\\'))*2), substring));
                        Console.WriteLine(res.Last());
                    }
                }
            }
            return res;
        }
        public static int HowMany(string s, char pattern)
        {
            int counter = 0;
            foreach(char c in s)
            {
                if (c == pattern)
                    counter++;
            }
            return counter;
        }
        public static List<string> GetAllPaths(string sDir)
        {
            List<string> dirs = new List<string>();
            try
            {
                dirs.Add(sDir);

                foreach (string f in Directory.GetFiles(sDir))
                {
                    dirs.Add(f);
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    dirs.AddRange(GetAllPaths(d));
                }
            }
            catch (System.Exception excpt)
            {
            }
            return dirs;
        }
    }
}