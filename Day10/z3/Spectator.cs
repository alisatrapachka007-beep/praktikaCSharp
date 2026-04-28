public class Spectator : IPlayerObserver
{
    private string _name;

    public Spectator(string name)
    {
        _name = name;
    }

    public void Update(string status)
    {
        Console.WriteLine($"Зритель {_name}: Получил обновление - {status}");
    }
}
