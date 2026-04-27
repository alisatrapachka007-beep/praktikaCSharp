using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        string dir = @"C:\учеба\praktikaCSharp\Day9\z4\watch";
        Directory.CreateDirectory(dir);

        Console.WriteLine($"Отслеживание: {dir}\n");

        using (var watcher = new FileSystemWatcher(dir))
        {
            watcher.Created += (s, e) =>
            {
                Console.WriteLine($"+ {e.Name}");

                string name = Path.GetFileNameWithoutExtension(e.Name);
                string ext = Path.GetExtension(e.Name);
                string newPath = e.FullPath;
                int copy = 1;

                while (File.Exists(newPath))
                {
                    newPath = Path.Combine(dir, $"{name}_copy{copy}{ext}");
                    copy++;
                }

                if (newPath != e.FullPath)
                {
                    File.Move(e.FullPath, newPath);
                    Console.WriteLine($"  -> {Path.GetFileName(newPath)}");
                }
            };

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("3 файла test.txt\n");

            for (int i = 0; i < 3; i++)
            {
                string testFile = Path.Combine(dir, "test.txt");
                File.WriteAllText(testFile, $"Содержимое {i + 1}");
                Thread.Sleep(100);
            }

            Thread.Sleep(500);

            Console.WriteLine("\nРезультат:");
            foreach (var f in Directory.GetFiles(dir))
                Console.WriteLine($"  {Path.GetFileName(f)}");

            foreach (var f in Directory.GetFiles(dir))
                File.Delete(f);
            Directory.Delete(dir);
        }
    }
}