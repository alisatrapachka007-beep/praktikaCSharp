string s = "abcabcbb";
Console.WriteLine("Строка: " + s);

string maxSub = "";

for (int i = 0; i < s.Length; i++)
{
    string cur = "";
    for (int j = i; j < s.Length; j++)
    {
        if (cur.Contains(s[j]))
        {
            break;
        }
        cur = cur + s[j];
        if (cur.Length > maxSub.Length)
        {
            maxSub = cur;
        }
    }
}

Console.WriteLine("Результат: " + maxSub);
Console.WriteLine("Длина: " + maxSub.Length);