using System;

namespace GenericsTask
{
    public interface IEvent<T>
    {
        void Trigger(T data);
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleEvent<string> simpleEvent = new SimpleEvent<string>();
            EventManager<string> manager = new EventManager<string>(simpleEvent);
            EventProcessor<string> processor = new EventProcessor<string>(manager);

            processor.ProcessAndRaise("Тестовое событие");
            Console.WriteLine($"Последние данные: {manager.GetLastData()}");
        }
    }
}