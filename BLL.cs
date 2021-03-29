using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectThree
{
    class BLL
    {
        static int count = 0;
        static string _filename;
        static string _dir;
        static readonly DAL dAL = new DAL();
        public void SearchAllDrives(string file, string dir)
        {
            _filename = file;
            _dir = dir;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string[] drives = dir.Split(':');

            if (drives.Length == 1)
            {
                foreach (DriveInfo d in allDrives)
                {
                    Search(d, dir);
                    if (count == 0) Console.WriteLine("nothing found in this directory.");
                    count = 0;
                }
            }
            else
            {
                var dr = (from x in allDrives
                          where x.Name == drives[0].ToUpper() + ":\\"
                          select x).ToList();
                Search(dr[0], dir);
                if (count == 0) Console.WriteLine("nothing found in this directory.");
            }
        }


        private void Search(DriveInfo drive, string dir)
        {
            Console.WriteLine("Searching in Drive {0}...", dir == "" ? drive.Name : dir);
            if (drive.IsReady == true)
            {
                if (dir == "")
                {
                    GetFileFromFolder(_filename, drive.Name);

                    foreach (FileSystemInfo foundFile in FindFilesAndDirs(drive.Name))
                    {
                        string FolderName = foundFile.FullName;

                        GetFolder(FolderName);
                    }
                }
                else
                {
                    GetFolder(dir);
                }
            }
        }


        private FileSystemInfo[] FindFilesAndDirs(string LocalDrive)
        {
            try
            {
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(LocalDrive);
                FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos();
                return filesAndDirs;

            }
            catch (Exception)
            {
                FileSystemInfo[] filesAndDirs = { };
                return filesAndDirs; ;
            }
        }

        private void GetFolder(string FolderName)
        {
            try
            {
                var responsePath = Directory.GetDirectories(FolderName);
                if (responsePath.Length == 0) return;
                foreach (FileSystemInfo foundDir in FindFilesAndDirs(FolderName))
                {
                    GetFileFromFolder(_filename, foundDir.FullName);
                }
                GetFolder(responsePath[0]);
            }
            catch (Exception)
            { }
        }

        private void GetFileFromFolder(string filename, string dirname)
        {
            List<string> dirs;
            FileSystemInfo[] sysdirs;
            if (Path.HasExtension(dirname) && !dirname.Contains(_filename))
            {
                return;
            }
            else
            {
                sysdirs = FindFilesAndDirs(dirname);
            }
            try
            {
                if (sysdirs.Length == 0)
                {
                    dirs = Directory.GetFiles(Path.HasExtension(dirname) ? Path.GetDirectoryName(dirname) : dirname, "*" + filename + "*").ToList();
                }
                else
                {
                    dirs = new List<string>();
                    for (int i = 0; i < sysdirs.Length; i++)
                    {
                        if (sysdirs[i].FullName.Contains(_filename)) dirs.Add(sysdirs[i].FullName);
                    }
                }

                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                    dAL.SQLFactory(_filename,_dir,dir);
                    count++;
                }

            }
            catch (Exception e)
            { }
        }


    }
}
