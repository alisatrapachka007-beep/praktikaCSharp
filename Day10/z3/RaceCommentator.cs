public class RaceCommentator : IPlayerObserver
{
    private string _name;

    public RaceCommentator(string name)
    {
        _name = name;
    }

    public void Update(string status)
    {
        Console.WriteLine($"Комментатор {_name}: Комментирует - {status}");
    }
}
