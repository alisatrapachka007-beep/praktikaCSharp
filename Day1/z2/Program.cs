using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int a = number / 100;
        int b = (number / 10) % 10;
        int c = number % 10;

        bool result = (a < b) && (b < c);

        if (result)
            Console.WriteLine("Истина");
        else
            Console.WriteLine("Ложь");
    }
}