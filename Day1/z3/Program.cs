using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите N (1-10): ");
        int N = Convert.ToInt32(Console.ReadLine());

        int sum = 0;

        for (int i = N; i <= 2 * N; i++)
        {
            sum = sum + i * i;
        }

        Console.WriteLine(sum);
    }
}