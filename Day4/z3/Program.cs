using System;

class Program
{
    static bool IsSorted(int[] arr, int index = 0)
    {
        if (arr == null || arr.Length <= 1)
            return true;

        if (index >= arr.Length - 1)
            return true;

        if (arr[index] > arr[index + 1])
            return false;

        return IsSorted(arr, index + 1);
    }

    static void Main()
    {
        int[] arr1 = { 1, 2, 3, 4 };
        int[] arr2 = { 1, 3, 2, 4 };
        int[] arr3 = { 5 };
        int[] arr4 = null;

        Console.WriteLine(IsSorted(arr1)); // True
        Console.WriteLine(IsSorted(arr2)); // False
        Console.WriteLine(IsSorted(arr3)); // True
        Console.WriteLine(IsSorted(arr4)); // True
    }
}