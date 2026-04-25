using System;

class ObjectAccessException : Exception
{
    public ObjectAccessException() : base() { }

    public ObjectAccessException(string message) : base(message) { }

    public ObjectAccessException(string message, Exception innerException)
        : base(message, innerException) { }
}

class ObjectManager
{
    public void AccessObject(object obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("Попытка доступа к null объекту.");
        }

        Console.WriteLine($"Доступ к объекту {obj.GetType().Name} успешен.");
    }
}

class ObjectHandler
{
    private ObjectManager _objectManager = new ObjectManager();

    public void HandleObjectAccess(object obj)
    {
        try
        {
            _objectManager.AccessObject(obj);
        }
        catch (NullReferenceException ex)
        {
            throw new ObjectAccessException("Ошибка доступа к объекту.", ex);
        }
    }
}

class Program
{
    static void Main()
    {
        ObjectHandler handler = new ObjectHandler();

        try
        {
            handler.HandleObjectAccess(null);
        }
        catch (ObjectAccessException ex)
        {
            Console.WriteLine("Информация об исключении");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Стек вызовов:{ex.StackTrace}");
            Console.WriteLine($"Тип исключения: {ex.GetType().Name}");

            if (ex.InnerException != null)
            {
                Console.WriteLine("Внутреннее исключение");
                Console.WriteLine($"Сообщение InnerException: {ex.InnerException.Message}");
                Console.WriteLine($"Тип InnerException: {ex.InnerException.GetType().Name}");
                Console.WriteLine($"Стек вызовов InnerException:{ex.InnerException.StackTrace}");
            }
        }
    }
}