using System.Text;

StringBuilder sb = new StringBuilder("привет мир");
string word = "мир";

Console.WriteLine("StringBuilder: " + sb);
Console.WriteLine("Поиск: " + word);

int index = sb.ToString().IndexOf(word);

if (index != -1)
{
    Console.WriteLine("Позиция: " + index);
}
else
{
    Console.WriteLine("Не найдено");
}