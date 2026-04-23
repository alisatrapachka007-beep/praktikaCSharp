using System;

abstract class Ticket
{
    public abstract double GetPrice();
}

class Standard : Ticket
{
    public override double GetPrice()
    {
        return 10.0;
    }
}

class VIP : Ticket
{
    public override double GetPrice()
    {
        return 25.0;
    }
}

class Student : Ticket
{
    public override double GetPrice()
    {
        return 7.0;
    }
}

class Program
{
    static void Main()
    {
        Ticket[] tickets = new Ticket[3];

        tickets[0] = new Standard();
        tickets[1] = new VIP();
        tickets[2] = new Student();

        for (int i = 0; i < tickets.Length; i++)
        {
            Console.WriteLine($"Билет {i + 1}: {tickets[i].GetPrice():F2} руб.");
        }
    }
}