namespace CylinderSurfaceArea;

internal class Program
{
    private static void Main()
    {
        Console.Write("Радиус основания (см) -> ");
        double radius = Convert.ToDouble(Console.ReadLine());

        Console.Write("Высота цилиндра (см) -> ");
        double height = Convert.ToDouble(Console.ReadLine());

        double surfaceArea = 2 * Math.PI * radius * (radius + height);

        Console.WriteLine($"Площадь поверхности цилиндра: {surfaceArea:F2} кв.см.");
    }
}