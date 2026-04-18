int[] numbers = new int[100];
Random rand = new Random();

for (int i = 0; i < 100; i++)
{
    numbers[i] = rand.Next(-500, 500);
}

Console.WriteLine("Исходный массив:");
for (int i = 0; i < numbers.Length; i++)
{
    Console.Write(numbers[i] + " ");
}
Console.WriteLine();

Array.Sort(numbers);

Console.WriteLine("Отсортированный массив:");
for (int i = 0; i < numbers.Length; i++)
{
    Console.Write(numbers[i] + " ");
}
Console.WriteLine();

Console.Write("Введите число k: ");
int k = int.Parse(Console.ReadLine());

int index = Array.BinarySearch(numbers, k);

if (index >= 0)
{
    Console.WriteLine("Число " + k + " найдено на позиции " + index);
}
else
{
    Console.WriteLine("Число " + k + " не найдено");
}

int min = numbers[0];
int max = numbers[99];
int minPos = 0;
int maxPos = 99;
int sum = 0;
int count = 0;

for (int i = minPos; i <= maxPos; i++)
{
    sum = sum + numbers[i];
    count = count + 1;
}

double average = (double)sum / count;

Console.WriteLine("Минимальное число: " + min);
Console.WriteLine("Максимальное число: " + max);
Console.WriteLine("Среднее арифметическое: " + average);