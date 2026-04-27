using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

        using (var w = new StreamWriter(file))
        {
            w.WriteLine("ФИО, Должность,Зарплата");
            foreach (var e in employees)
                w.WriteLine($"{e.Name}, {e.Position}, {e.Salary}");
        }

        var reader = new EmployeeFileReader(file);
        var all = reader.ReadEmployees();
        var filtered = new EmployeeProcessor().FilterBySalary(all, 70000);

        Console.WriteLine("Все сотрудники:");
        foreach (var e in all) Console.WriteLine($"{e.Name} - {e.Salary}");

        Console.WriteLine("\nС зарплатой >= 70000:");
        foreach (var e in filtered) Console.WriteLine($"{e.Name} - {e.Salary}");
    }
}