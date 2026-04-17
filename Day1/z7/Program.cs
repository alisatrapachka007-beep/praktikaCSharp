using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        int A = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите B: ");
        int B = Convert.ToInt32(Console.ReadLine());

        for (int i = A; i <= B; i++)
        {
            if (i > 0)
                Console.WriteLine(i);
        }
    }
}