using System;

class Program
{
    static void Main()
    {
        SubscriptionFactory factory = new PremiumFactory();
        ISubscription sub = factory.CreateSubscription();
        Console.WriteLine(sub.GetBenefits());
    }
}