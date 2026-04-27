namespace GenericsTask
{
    // Класс-хранилище (репозиторий)
    public class EventManager<T>
    {
        private IEvent<T> _event;
        private T _lastData;

        public EventManager(IEvent<T> eventHandler)
        {
            _event = eventHandler;
        }

        public void RaiseEvent(T data)
        {
            _lastData = data;
            _event.Trigger(data);
        }

        public T GetLastData() => _lastData;
    }
}