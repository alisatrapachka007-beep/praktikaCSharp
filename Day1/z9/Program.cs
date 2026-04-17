using System;

class Program
{
    static void Main()
    {
        double A = 0.1, B = 2.1;
        int M = 20;
        double H = (B - A) / M;
        double x = A;

        for (int i = 1; i <= M; i++)
        {
            double y = Math.Sin(1 / x) + 2 / x;
            Console.WriteLine(x + "    " + y);
            x += H;
        }
    }
}