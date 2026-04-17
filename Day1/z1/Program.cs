using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите длину стороны a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите длину стороны b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        double perimeter = 2 * (a + b);

        double diagonal = Math.Sqrt(a * a + b * b);

        Console.WriteLine($"\nПериметр прямоугольника:" + perimeter);
        Console.WriteLine($"Длина диагонали прямоугольника:" + diagonal);
    }
}