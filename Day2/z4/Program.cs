int[,] building = new int[12, 4];
Random rand = new Random();

for (int floor = 0; floor < 12; floor++)
{
    for (int flat = 0; flat < 4; flat++)
    {
        building[floor, flat] = rand.Next(1, 6);
    }
}

Console.WriteLine("Исходные данные (этаж: жильцы в 4 квартирах):");
for (int floor = 0; floor < 12; floor++)
{
    Console.Write((floor + 1) + "-й этаж: ");
    for (int flat = 0; flat < 4; flat++)
    {
        Console.Write(building[floor, flat] + " ");
    }
    Console.WriteLine();
}

int sumFloor3 = 0;
int sumFloor5 = 0;

for (int flat = 0; flat < 4; flat++)
{
    sumFloor3 = sumFloor3 + building[2, flat];
    sumFloor5 = sumFloor5 + building[4, flat];
}

Console.WriteLine("Всего жильцов на 3-м этаже: " + sumFloor3);
Console.WriteLine("Всего жильцов на 5-м этаже: " + sumFloor5);

if (sumFloor3 > sumFloor5)
{
    Console.WriteLine("На 3-м этаже проживает больше людей, чем на 5-м");
}
else if (sumFloor5 > sumFloor3)
{
    Console.WriteLine("На 5-м этаже проживает больше людей, чем на 3-м");
}
else
{
    Console.WriteLine("На 3-м и 5-м этажах проживает одинаковое количество людей");
}