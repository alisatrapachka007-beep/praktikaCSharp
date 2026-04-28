using System;
class Program
{
    static void Main()
    {
        var controller1 = ModeController.Instance;
        var controller2 = ModeController.Instance;

        Console.WriteLine($"controller1 == controller2: {controller1 == controller2}");

        Console.WriteLine($"Текущий режим: {controller1.GetMode()}");

        controller1.SetMode("отладочный");
        Console.WriteLine($"Новый режим: {controller2.GetMode()}");
    }
}