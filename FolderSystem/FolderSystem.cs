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
            int counterHolder = 1;
            string substring = "";
            int lastIndexOfSlash = 0;
            string LastFolder = "";
            Dictionary<string, int> occurences = new Dictionary<string, int>();
            Dictionary<string, int> folders = new Dictionary<string, int>();

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
                    FileAttributes file = File.GetAttributes(path);
                    if(!file.HasFlag(FileAttributes.Directory))
                    {
                        if (!occurences.ContainsKey(path.Substring(0, lastIndexOfSlash)))
                        {
                            res.Add(path);
                            occurences.Add(path, 1);
                            
                            Console.WriteLine(res.Last());
                        }
                        else
                        {
                            res.Add(path);
                            Console.WriteLine(res.Last());
                        }
                    }
                    
                    else
                    {
                        //if (LastFolder != path.Split('\\')[path.Split('\\').Count() - 2])
                        //{
                        //    LastFolder = path.Split('\\')[path.Split('\\').Count() - 2];
                        //    res.Add(path.Substring(0,lastIndexOfSlash));
                        //    Console.WriteLine(res.Last());

                        //}
                        substring = path.Substring(lastIndexOfSlash, path.Length - lastIndexOfSlash );
                        res.Add(string.Format("{0} {1}", new string('.', HowMany(path, '\\')), substring));
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