using System;

class Program
{
    static void GetValues(out int a, out double b)
    {
        a = 0;
        b = 0.0;
    }

    static void GetValues(out string name, out int age)
    {
        name = "John";
        age = 30;
    }

    static void Main()
    {
        GetValues(out int num, out double dec);
        Console.WriteLine($"int: {num}, double: {dec}");

        GetValues(out string personName, out int personAge);
        Console.WriteLine($"name: {personName}, age: {personAge}");
    }
}