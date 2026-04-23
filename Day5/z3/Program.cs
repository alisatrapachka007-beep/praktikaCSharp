using System;
using System.Collections.Generic;

class ArtPiece
{
    public string Title { get; set; }
    public ArtPiece(string title) { Title = title; }
}

interface IPainting
{
    void Paint();
}

interface ISculpture
{
    void Sculpt();
}

class Portrait : ArtPiece, IPainting
{
    public Portrait(string title) : base(title) { }
    public void Paint() { Console.WriteLine($"Картина {Title} написана красками"); }
}

class Statue : ArtPiece, ISculpture
{
    public Statue(string title) : base(title) { }
    public void Sculpt() { Console.WriteLine($"Скульптура {Title} высечена из камня"); }
}

class Program
{
    static void Main()
    {
        ArtPiece[] pieces = new ArtPiece[]
        {
            new Portrait("Мона Лиза"),
            new Statue("Давид"),
            new Portrait("Звездная ночь"),
            new Statue("Венера Милосская"),
            new Portrait("Крик")
        };

        Console.WriteLine("Все скульптуры:");
        foreach (var piece in pieces)
        {
            if (piece is ISculpture sculpture)
            {
                sculpture.Sculpt();
            }
        }
    }
}