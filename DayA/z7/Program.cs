using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите сторону a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону c: ");
        double c = double.Parse(Console.ReadLine());

        double p = (a + b + c) / 2;

        double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

        double height = 2 * area / a;

        Console.WriteLine($"Высота, опущенная на сторону a: {height:F2}");
    }
}