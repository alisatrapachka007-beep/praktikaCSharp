using System;
using System.Collections.Generic;

namespace ObservableCollectionTask
{
    class Program
    {
        static void Main(string[] args)
        {
            MyObservableList<string> list = new MyObservableList<string>();
            ObservableListManager<string> manager = new ObservableListManager<string>();

            manager.Subscribe(list);

            list.Add("Car1");
            list.Add("Car2");
            list.Remove("Car1");

            Console.WriteLine($"Содержит Car2: {list.Contains("Car2")}");
            Console.WriteLine($"Количество: {list.Count}");
        }
    }
}