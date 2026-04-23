using System;

class Program
{
    static void SortInc3(ref double A, ref double B, ref double C)
    {
        if (A > B)
        {
            double temp = A;
            A = B;
            B = temp;
        }
        if (A > C)
        {
            double temp = A;
            A = C;
            C = temp;
        }
        if (B > C)
        {
            double temp = B;
            B = C;
            C = temp;
        }
    }

    static void Main()
    {
        double A1 = 5.5, B1 = 2.1, C1 = 8.3;
        double A2 = 3.3, B2 = 7.7, C2 = 1.1;

        Console.WriteLine($"Первый набор: A1={A1}, B1={B1}, C1={C1}");
        SortInc3(ref A1, ref B1, ref C1);
        Console.WriteLine($"После сортировки: A1={A1}, B1={B1}, C1={C1}");
        Console.WriteLine($"Второй набор: A2={A2}, B2={B2}, C2={C2}");
        SortInc3(ref A2, ref B2, ref C2);
        Console.WriteLine($"После сортировки: A2={A2}, B2={B2}, C2={C2}");
    }
}