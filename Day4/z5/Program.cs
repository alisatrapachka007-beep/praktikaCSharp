using System;

public abstract class Computer
{
    public abstract void Start();

    public virtual void Shutdown()
    {
        Console.WriteLine("Computer is shutting down...");
    }
}

public class Desktop : Computer
{
    public override void Start()
    {
        Console.WriteLine("Desktop is starting");
    }

    public override void Shutdown()
    {
        Console.WriteLine("Desktop is shutting down...");
    }
}

public class Laptop : Computer
{
    public override void Start()
    {
        Console.WriteLine("Laptop is starting");
    }

    public override void Shutdown()
    {
        Console.WriteLine("Laptop is shutting down...");
    }
}

class Program
{
    static void Main()
    {
        Computer pc1 = new Desktop();
        Computer pc2 = new Laptop();

        pc1.Start();
        pc1.Shutdown();

        Console.WriteLine();

        pc2.Start();
        pc2.Shutdown();
    }
}