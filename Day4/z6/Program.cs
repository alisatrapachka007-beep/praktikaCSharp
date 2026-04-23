using System;

class Program
{
    static int DaysInYear(int Y)
    {
        if (Y <= 0)
            throw new ArgumentException("Год должен быть положительным числом");

        bool isLeap = false;

        if (Y % 4 == 0)
        {
            if (Y % 100 == 0)
            {
                if (Y % 400 == 0)
                    isLeap = true;
                else
                    isLeap = false;
            }
            else
            {
                isLeap = true;
            }
        }

        return isLeap ? 366 : 365;
    }

    static void Main()
    {
        int[] years = { 2020, 2021, 1900, 2000, 2024 };

        foreach (int year in years)
        {
            Console.WriteLine($"Год {year}: {DaysInYear(year)} дней");
        }
    }
}