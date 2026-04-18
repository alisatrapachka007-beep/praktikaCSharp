using System.Text.RegularExpressions;

string url = "https://classroom.google.com/u/0/c/ODU5OTM2ODYxNDY0/a/ODYwMzE0ODMyNzc1/details";

Console.WriteLine("URL: " + url);

string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";

bool isValid = Regex.IsMatch(url, pattern);

if (isValid)
{
    Console.WriteLine("URL корректный");
}
else
{
    Console.WriteLine("URL некорректный");
}