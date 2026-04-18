string text = "Привет приветик приветули хай здравствуйте здарова";
int length = 10;

Console.WriteLine("Исходная строка: " + text);
Console.WriteLine("Длина фрагмента: " + length);

for (int i = 0; i < text.Length; i = i + length)
{
    if (i + length < text.Length)
    {
        Console.WriteLine(text.Substring(i, length));
    }
    else
    {
        Console.WriteLine(text.Substring(i));
    }
}