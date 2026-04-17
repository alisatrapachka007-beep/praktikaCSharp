using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите номер карты (6-14): ");
        int k = Convert.ToInt32(Console.ReadLine());

        switch (k)
        {
            case 6:
                Console.WriteLine("шестерка");
                break;
            case 7:
                Console.WriteLine("семерка");
                break;
            case 8:
                Console.WriteLine("восьмерка");
                break;
            case 9:
                Console.WriteLine("девятка");
                break;
            case 10:
                Console.WriteLine("десятка");
                break;
            case 11:
                Console.WriteLine("валет");
                break;
            case 12:
                Console.WriteLine("дама");
                break;
            case 13:
                Console.WriteLine("король");
                break;
            case 14:
                Console.WriteLine("туз");
                break;
            default:
                Console.WriteLine("Некорректный номер карты");
                break;
        }
    }
}