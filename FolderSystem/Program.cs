using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using FolderSystem;
namespace FolderSystem
{
    class Program
    {

        static void Main(string[] args)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            int option = 0;

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                if (d.IsReady == true)
                {
                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }
            Console.WriteLine("Enter a number of a drive section to be listed:\n");
            option = Int32.Parse(Console.ReadLine());
            List<string> s = FolderSystem.GetAllPaths(allDrives[option -1].RootDirectory.FullName);
            FolderSystem.ShowStructure(s);
        }
    }

}
