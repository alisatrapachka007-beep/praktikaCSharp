string text = "привет мир";
string word = "мир";

Console.WriteLine("Строка: " + text);
Console.WriteLine("Искомое слово: " + word);

if (text.Contains(word))
{
    Console.WriteLine("Строка содержит подстроку \"" + word + "\"");
}
else
{
    Console.WriteLine("Строка не содержит подстроку \"" + word + "\"");
}