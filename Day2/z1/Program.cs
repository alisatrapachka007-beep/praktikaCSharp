int[] numbers = { -5, 3, -2, 7, 0, -1, 8, -4, 6 };
int positiveCount = 0;

for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] > 0)
    {
        Console.Write($"{numbers[i]} ");
        positiveCount++;
    }
}

Console.WriteLine($"\nКоличество положительных элементов: {positiveCount}");