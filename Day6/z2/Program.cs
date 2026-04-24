using System;

delegate void OrderHandler(double amount);

class Shop
{
    public void HandleOrder(double amount, OrderHandler handler)
    {
        Console.WriteLine($"Заказ на сумму: {amount} руб.");
        handler(amount);
    }
}

class Program
{
    static void ApplyDiscount(double amount)
    {
        double discount = amount * 0.1;
        Console.WriteLine($"Скидка 10%: -{discount} руб. Итого: {amount - discount} руб.");
    }

    static void CalculateTax(double amount)
    {
        double tax = amount * 0.2;
        Console.WriteLine($"Налог 20%: +{tax} руб. Итого: {amount + tax} руб.");
    }

    static void Main()
    {
        Shop shop = new Shop();

        Console.WriteLine("С применением скидки:");
        shop.HandleOrder(1000, ApplyDiscount);

        Console.WriteLine("\n С расчётом налога:");
        shop.HandleOrder(1000, CalculateTax);

        Console.WriteLine("\n Скидка + налог:");
        OrderHandler combined = ApplyDiscount;
        combined += CalculateTax;
        shop.HandleOrder(1000, combined);
    }
}