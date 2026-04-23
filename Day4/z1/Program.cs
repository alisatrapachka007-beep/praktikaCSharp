using System;

public static class Zadanie
{
    public static string ReverseString(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

class Program
{
    static void Main()
    {
        string result = Zadanie.ReverseString("cи шарп");
        Console.WriteLine(result);
    }
}