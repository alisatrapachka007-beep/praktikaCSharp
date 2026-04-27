using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string dir = @"C:\учеба\praktikaCSharp\Day9\z2";
        Directory.CreateDirectory(dir);
        string file = Path.Combine(dir, "file.data");

        var employees = new List<Employee>
        {
            new Employee("Иванов И.И.", "Менеджер", 75000),
            new Employee("Петров П.П.", "Разработчик", 85000),
            new Employee("Сидоров С.С.", "Тестировщик", 65000)
        };

        var writer = new EmployeeFileWriter(file);
        writer.WriteEmployeesWithHeader(employees);

        Console.WriteLine("Содержимое файла:");
        Console.WriteLine(File.ReadAllText(file));
    }
}