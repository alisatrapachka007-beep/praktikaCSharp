using System;
using System.Globalization;

class Program
{
    static string ToString(int number)
    {
        return number.ToString();
    }

    static string ToString(double number)
    {
        return number.ToString(CultureInfo.InvariantCulture);
    }

    static void Main()
    {
        int intNum = 42;
        double doubleNum = 3.14;

        Console.WriteLine(ToString(intNum));
        Console.WriteLine(ToString(doubleNum));
    }
}