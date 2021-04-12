using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
namespace FolderSystem
{
    public class Folder
    {
        public string Name { get; set; }
        public List<Folder> Folders { get; set; } = new List<Folder>();
        public List<File> Files { get; set; } = new List<File>();
    }

    public class File
    {
        public string Name { get; set; }
    }

    public class FolderSystem
    {
        public static List<Folder> GetFoldersFormStrings(List<string> strings)
        {
            var folders = new List<Folder>();
            strings.Sort(StringComparer.InvariantCultureIgnoreCase);
            var folderByPath = new Dictionary<string, Folder>();
            foreach (var str in strings)
            {
                if (str.EndsWith("/")) // we have a folder
                {
                    EnsureFolder(folders, folderByPath, str);
                }
                else // we have a file
                {
                    var lastSlashPosition = str.LastIndexOf("/");
                    var parentFolderPath = str.Substring(0, lastSlashPosition + 1);
                    var parentFolder = EnsureFolder(folders, folderByPath, parentFolderPath);
                    var fileName = str.Substring(lastSlashPosition + 1);
                    var file = new File
                    {
                        Name = fileName
                    };
                    parentFolder.Files.Add(file);
                }
            }
            return folders;
        }

        public static Folder EnsureFolder(List<Folder> rootFolders, Dictionary<string, Folder> folderByPath, string folderPath)
        {
            if (!folderByPath.TryGetValue(folderPath, out var folder))
            {
                var folderPathWithoutEndSlash = folderPath.TrimEnd('/');
                var lastSlashPosition = folderPathWithoutEndSlash.LastIndexOf("/");
                List<Folder> folders;
                string folderName;
                if (lastSlashPosition < 0) // it's a first level folder
                {
                    folderName = folderPathWithoutEndSlash;
                    folders = rootFolders;
                }
                else
                {
                    var parentFolderPath = folderPath.Substring(0, lastSlashPosition + 1);
                    folders = folderByPath[parentFolderPath].Folders;
                    folderName = folderPathWithoutEndSlash.Substring(lastSlashPosition + 1);
                }
                folder = new Folder
                {
                    Name = folderName
                };
                folders.Add(folder);
                folderByPath.Add(folderPath, folder);
            }
            return folder;
        }

        public static void ShowFolders(List<Folder> folders)
        {
            foreach (var folder in folders)
            {
                ShowFolder(folder, 0);
            }
        }

        public static void ShowFolder(Folder folder, int indentation)
        {
            string folderIndentation = new string(' ', indentation);
            string fileIndentation = folderIndentation + "  ";
            Console.WriteLine($"{folderIndentation}-{folder.Name}");
            foreach (var file in folder.Files)
            {
                Console.WriteLine($"{fileIndentation}-{file.Name}");
            }
            foreach (var subfolder in folder.Folders)
            {
                ShowFolder(subfolder, indentation + 2);
            }
        }
        public static List<string> DirSearch_ex3(string sDir)
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
                    dirs.AddRange(DirSearch_ex3(d));
                }
            }
            catch (System.Exception excpt)
            {
            }
            return dirs;
        }
    }
}