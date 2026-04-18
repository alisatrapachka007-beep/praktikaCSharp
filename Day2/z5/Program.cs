int[][] jagged = new int[4][];
Random rand = new Random();

for (int i = 0; i < 4; i++)
{
    jagged[i] = new int[rand.Next(3, 6)];
    for (int j = 0; j < jagged[i].Length; j++)
    {
        jagged[i][j] = rand.Next(1, 10);
    }
}

Console.WriteLine("Ступенчатый массив:");
for (int i = 0; i < jagged.Length; i++)
{
    Console.Write(i + ": ");
    for (int j = 0; j < jagged[i].Length; j++)
    {
        Console.Write(jagged[i][j] + " ");
    }
    Console.WriteLine();
}

for (int i = 0; i < jagged.Length; i++)
{
    int sum = 0;
    for (int j = 0; j < jagged[i].Length; j++)
    {
        sum = sum + jagged[i][j];
    }

    bool prime = true;
    if (sum < 2) prime = false;
    for (int d = 2; d < sum; d++)
    {
        if (sum % d == 0) prime = false;
    }

    if (prime)
    {
        Console.WriteLine("Строка " + i + " сумма=" + sum + " простое число");
    }
}