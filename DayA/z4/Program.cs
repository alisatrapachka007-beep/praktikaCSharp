namespace ArithmeticOperations;

internal class Program
{
    private static void Main()
    {
        double a = 12.4;
        double b = 2.567;
        double c = 100;

        Console.WriteLine($"({a:F4}+({b:F4}+{c:F4})) = ({a:F4}+{c:F4}+{b:F4})");

    }
}