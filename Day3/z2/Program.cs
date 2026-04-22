using System;
using System.Collections.Generic;

class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}

static class StringProcessor
{
    public static string ConcatenateNames(Person[] persons)
    {
        string result = "";

        for (int i = 0; i < persons.Length; i++)
        {
            result += persons[i].Name;

            if (i < persons.Length - 1)
            {
                result += ", ";
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        Person[] people = new Person[]
        {
            new Person("Анна"),
            new Person("Иван"),
            new Person("Мария"),
            new Person("Петр")
        };

        string names = StringProcessor.ConcatenateNames(people);
        Console.WriteLine(names);
    }
}