int N;
do
{
    Console.Write("Введите N (N < 10): ");
    N = int.Parse(Console.ReadLine());
}
while (N >= 10);

int a, b;
Console.Write("Введите a: ");
a = int.Parse(Console.ReadLine());
Console.Write("Введите b: ");
b = int.Parse(Console.ReadLine());

int[,] matrix = new int[N, N];
Random rand = new Random();

for (int i = 0; i < N; i++)
{
    for (int j = 0; j < N; j++)
    {
        matrix[i, j] = rand.Next(a, b + 1);
    }
}

Console.WriteLine("Исходная матрица:");
for (int i = 0; i < N; i++)
{
    for (int j = 0; j < N; j++)
    {
        Console.Write(matrix[i, j] + "\t");
    }
    Console.WriteLine();
}

int K, L;
Console.Write("Введите K: ");
K = int.Parse(Console.ReadLine());
Console.Write("Введите L: ");
L = int.Parse(Console.ReadLine());

int sum = 0;
for (int i = 0; i < N; i++)
{
    for (int j = 0; j < N; j++)
    {
        if (matrix[i, j] >= K && matrix[i, j] < L)
        {
            sum = sum + matrix[i, j];
        }
    }
}
Console.WriteLine("Сумма чисел из промежутка [" + K + ", " + L + "): " + sum);

int k;
Console.Write("Введите номер столбца k: ");
k = int.Parse(Console.ReadLine());

int max = matrix[0, k];
for (int i = 1; i < N; i++)
{
    if (matrix[i, k] > max)
    {
        max = matrix[i, k];
    }
}
Console.WriteLine("Наибольший элемент " + k + "-го столбца: " + max);