using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите сторону a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите сторону c: ");
        double c = Convert.ToDouble(Console.ReadLine());

        bool isIsosceles = (a == b) || (a == c) || (b == c);

        if (isIsosceles)
            Console.WriteLine("Треугольник равнобедренный");
        else
            Console.WriteLine("Треугольник не равнобедренный");
    }
}