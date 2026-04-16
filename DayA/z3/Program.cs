double m, n;
Console.Write("Введите m: ");
m = double.Parse(Console.ReadLine());
Console.Write("Введите n: ");
n = double.Parse(Console.ReadLine());

double z1 = ((m - 1) * Math.Sqrt(m) - (n - 1) * Math.Sqrt(n))
            / (Math.Sqrt(Math.Pow(m, 3) * n) + n * m + m * m - m);

double z2 = (Math.Sqrt(m) - Math.Sqrt(n)) / m;

Console.WriteLine($"z1 = {z1}");
Console.WriteLine($"z2 = {z2}");
Console.WriteLine($"Разница: {z1 - z2}");