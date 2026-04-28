public class RaceGame
{
    private List<IPlayerObserver> _observers = new List<IPlayerObserver>();
    private string _raceStatus = "Гонка не начата";

    public void Subscribe(IPlayerObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IPlayerObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetRaceStatus(string status)
    {
        _raceStatus = status;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_raceStatus);
        }
    }
}
