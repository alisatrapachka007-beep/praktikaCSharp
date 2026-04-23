using System;

interface ICloudStorage
{
    void SaveData(string data);
}

interface ILocalStorage
{
    void SaveData(string data);
}

class StorageManager : ICloudStorage, ILocalStorage
{
    void ICloudStorage.SaveData(string data)
    {
        Console.WriteLine($"Облако: {data}");
    }

    void ILocalStorage.SaveData(string data)
    {
        Console.WriteLine($"Локально: {data}");
    }
}

class Program
{
    static void Main()
    {
        StorageManager manager = new StorageManager();

        ICloudStorage cloud = manager;
        ILocalStorage local = manager;

        cloud.SaveData("Документ");
        local.SaveData("Документ");
    }
}