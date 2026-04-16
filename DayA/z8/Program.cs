using System;

class Program
{
    static void Main()
    {
        double x = 1.3;
        double y = 2 * Math.Atan(Math.Sqrt(Math.Abs(1 - x * x))) + Math.Log(7 * x) / (1 + Math.Exp(x));

        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y:F6}");
    }
}