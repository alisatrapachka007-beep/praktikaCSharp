using System;
using System.IO;

class Program
{
    static void Main()
    {
        string dir = @"C:\учеба\praktikaCSharp\Day9\z1";
        Directory.CreateDirectory(dir);

        string file = Path.Combine(dir, "Trepachka.ap");
        string copy = Path.Combine(dir, "copy.ap");
        string testDir = Path.Combine(dir, "testdir");

        File.WriteAllText(file, "Трепачко А.П.\nГруппа: 40тп");
        Console.WriteLine($"Создан: {file}");

        Console.WriteLine($"Содержимое:\n{File.ReadAllText(file)}");

        var info = new FileInfo(file);
        Console.WriteLine($"Размер: {info.Length} байт, Создан: {info.CreationTime}");

        File.Copy(file, copy, true);
        Console.WriteLine($"Скопирован в {copy}");

        string moved = Path.Combine(dir, "newdir", "Trepachka.ap");
        Directory.CreateDirectory(Path.GetDirectoryName(moved));
        File.Move(file, moved);
        Console.WriteLine($"Перемещён в {moved}");

        string renamed = Path.Combine(dir, "Trepachka.io");
        File.Move(moved, renamed);
        File.Move(renamed, file);
        Console.WriteLine("Переименован");

        File.Delete(file);
        Console.WriteLine($"Удалён: {file}");

        long size1 = new FileInfo(copy).Length;
        long size2 = new FileInfo(file).Length;
        Console.WriteLine($"Сравнение: {(size1 > size2 ? "первый больше" : size1 < size2 ? "второй больше" : "равны")}");

        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test.ap"), "test");
        foreach (var f in Directory.GetFiles(testDir, "*.ap")) File.Delete(f);
        Console.WriteLine("Файлы .ap удалены");

        string prot = Path.Combine(testDir, "prot.txt");
        File.WriteAllText(prot, "текст");
        new FileInfo(prot).IsReadOnly = true;
        try { File.WriteAllText(prot, "новая запись"); }
        catch { Console.WriteLine("Запись запрещена"); }

        Directory.Delete(testDir, true);
        Directory.Delete(Path.Combine(dir, "newdir"), true);
        File.Delete(copy);
    }
}