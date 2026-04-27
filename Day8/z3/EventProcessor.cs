namespace GenericsTask
{
    // Класс бизнес-логики
    public class EventProcessor<T>
    {
        private EventManager<T> _manager;

        public EventProcessor(EventManager<T> manager)
        {
            _manager = manager;
        }

        public void ProcessAndRaise(T data)
        {
            Console.WriteLine($"Обработка: {data}");
            _manager.RaiseEvent(data);
        }
    }
}