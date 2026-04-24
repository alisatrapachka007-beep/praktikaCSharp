using System;

delegate void LightControl(bool turnOn);

class RoomLight
{
    public void ControlLight(bool turnOn)
    {
        if (turnOn)
            Console.WriteLine("Свет в комнате включен");
        else
            Console.WriteLine("Свет в комнате выключен");
    }
}

class StreetLight
{
    public void ControlLight(bool turnOn)
    {
        if (turnOn)
            Console.WriteLine("Уличный свет включен");
        else
            Console.WriteLine("Уличный свет выключен");
    }
}

class Program
{
    static void Main()
    {
        RoomLight room = new RoomLight();
        StreetLight street = new StreetLight();

        LightControl roomControl = room.ControlLight;
        LightControl streetControl = street.ControlLight;

        Console.WriteLine("Управление светом");

        roomControl(true);
        roomControl(false);

        Console.WriteLine();

        streetControl(true);
        streetControl(false);

        Console.WriteLine("Групповое управление");
        LightControl allLights = room.ControlLight;
        allLights += street.ControlLight;
        allLights(true);
    }
}