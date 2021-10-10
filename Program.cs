using System;
using System.IO;

namespace _8._6._3
{
    class Program
    {
        static long sum = 0;
        static void Main(string[] args)
        {
            Url();
            Console.WriteLine("Текущий размер папки: {0}", sum);
        }
        public static void Url()
        {
            Console.WriteLine("Пропишите путь до файла как в примере: C:/Users/user/Desktop/example.bin");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(filePath);
                    var folders = dir.GetDirectories();

                    WriteFileInfo(dir);
                    WriteFolderInfo(folders);

                    Console.WriteLine("Исходный размер папки: {0} байт", sum);

                    foreach(FileInfo file in dir.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach(DirectoryInfo di in dir.GetDirectories())
                    {
                        di.Delete(true);
                    }

                    Console.WriteLine("Освобождено: {0} байт", sum);
                    sum = 0;

                    WriteFileInfo(dir);
                    WriteFolderInfo(folders);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка {0}", ex.Message);
                }

            }

        }


        public static void WriteFolderInfo(DirectoryInfo[] folders)
        {
            Console.WriteLine();

            foreach (var folder in folders)
            {
                try
                {
                    sum += DirectoryExtension.DirSize(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(folder.Name + $" - Не удалось рассчитать размер: {e.Message}");
                }
            }
        }

        public static void WriteFileInfo(DirectoryInfo rootFolder)
        {
            foreach (var file in rootFolder.GetFiles())
            {
                sum += file.Length;
            }
        }

    }

    public static class DirectoryExtension
    {
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }

            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
