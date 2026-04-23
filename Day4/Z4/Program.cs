using System;

public static class StringExtensions
{
    public static string Reverse(this string str)
    {
        if (str == null)
            return null;

        char[] chars = str.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

class Program
{
    static void Main()
    {
        string text = "си шарп";
        string reversed = text.Reverse();

        Console.WriteLine($"Оригинал: {text}");
        Console.WriteLine($"Переворот: {reversed}");
    }
}