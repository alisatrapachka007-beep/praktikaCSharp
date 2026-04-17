using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        int A = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите B: ");
        int B = Convert.ToInt32(Console.ReadLine());

        int sum = 0;

        for (int i = A; i <= B; i++)
        {
            sum = sum + i;
        }

        Console.WriteLine(sum);
    }
}