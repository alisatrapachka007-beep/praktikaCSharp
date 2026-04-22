using System;

class A
{
    public int a;
    public int b;

    public A(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public double CalculateExpression()
    {
        if (a == 0)
        {
            Console.WriteLine("Ошибка: 'a' не может быть нулем!");
            return 0;
        }

        double result = (3 * b - 2.0 / (a * a)) / 4;
        return result;
    }

    public double CubeOfQuotient()
    {
        if (b == 0)
        {
            Console.WriteLine("Ошибка: 'b' не может быть нулем!");
            return 0;
        }

        double quotient = (double)a / b;
        double cube = Math.Pow(quotient, 3);
        return cube;
    }
}

class Program
{
    static void Main()
    {
        A obj = new A(2, 3);

        Console.WriteLine("Куб частного a/b: " + obj.CubeOfQuotient());
        Console.WriteLine($"a = {obj.a}, b = {obj.b}");
    }
}