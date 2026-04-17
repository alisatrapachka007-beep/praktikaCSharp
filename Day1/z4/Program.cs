using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите x: ");
        double x = Convert.ToDouble(Console.ReadLine());

        double y;

        if (x > 1 && x < 2)
        {
            y = (x - 2) * (x - 2) + 6;
        }
        else if (x >= 2)
        {
            y = Math.Log(x + 3 * Math.Sqrt(x));
        }
        else
        {
            Console.WriteLine("Функция не определена для x <= 1");
            return;
        }

        Console.WriteLine($"y = {y}");
    }
}