namespace GenericsTask
{
    // Реализация интерфейса
    public class SimpleEvent<T> : IEvent<T>
    {
        public void Trigger(T data)
        {
            Console.WriteLine($"Событие вызвано с данными: {data}");
        }
    }
}