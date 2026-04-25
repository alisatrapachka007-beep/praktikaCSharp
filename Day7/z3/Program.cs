using System;

class LowBatteryException : Exception
{
    public LowBatteryException() : base() { }

    public LowBatteryException(string message) : base(message) { }

    public LowBatteryException(string message, Exception innerException)
        : base(message, innerException) { }
}

class BatteryManager
{
    public void CheckBatteryLevel(int level)
    {
        if (level < 5)
        {
            throw new LowBatteryException($"Низкий уровень аккумулятора. Требуется зарядка");
        }

        Console.WriteLine($"Уровень заряда аккумулятора: {level}%. Работа продолжается");
    }
}

class Program
{
    static void Main()
    {
        BatteryManager batteryManager = new BatteryManager();

        Console.WriteLine("Проверка заряда 25%:");
        try
        {
            batteryManager.CheckBatteryLevel(25);
        }
        catch (LowBatteryException ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }

        Console.WriteLine();

        Console.WriteLine("Проверка заряда 3%:");
        try
        {
            batteryManager.CheckBatteryLevel(3);
        }
        catch (LowBatteryException ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }

    }
}