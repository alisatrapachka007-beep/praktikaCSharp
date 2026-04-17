using System;

class Program
{
    static void Main()
    {
        for (int i = 100; i <= 999; i++)
        {
            int square = i * i;
            if (square % 1000 == i)
            {
                Console.WriteLine(i);
            }
        }
    }
}