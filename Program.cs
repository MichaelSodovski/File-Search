using System;

namespace ProjectThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\t1. Enter file name to search");
            Console.WriteLine("\t2. Enter file name to search + parent directory to search in");
            Console.WriteLine("\t3. Exit");
            Console.Write("Your option? ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter file name to search: ");
                    string fileName = Console.ReadLine();
                    while (string.IsNullOrEmpty(fileName))
                    {
                        Console.WriteLine("File Name can't be empty! Input your file name once more");
                        fileName = Console.ReadLine();
                    }

                    BLL a = new BLL();
                    a.SearchAllDrives(fileName, "");
                    break;
                case "2":
                    Console.WriteLine("Enter file name to search: ");
                    string fileName1 = Console.ReadLine();
                    while (string.IsNullOrEmpty(fileName1))
                    {
                        Console.WriteLine("File Name can't be empty! Input your file name once more");
                        fileName1 = Console.ReadLine();
                    }
                    Console.WriteLine("Enter root directory to search in: ");
                    string dir = Console.ReadLine();
                    while (string.IsNullOrEmpty(dir))
                    {
                        Console.WriteLine("Root directory can't be empty! Input your root directory once more");
                        dir = Console.ReadLine();
                    }
                    BLL b = new BLL();
                    b.SearchAllDrives(fileName1, dir);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
