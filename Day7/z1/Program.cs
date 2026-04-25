using System;

class TooManyLoginAttemptsException : Exception
{
    public TooManyLoginAttemptsException() : base() { }

    public TooManyLoginAttemptsException(string message) : base(message) { }

    public TooManyLoginAttemptsException(string message, Exception innerException)
        : base(message, innerException) { }
}

class LoginManager
{
    public void AttemptLogin(int attempts)
    {
        if (attempts > 3)
        {
            throw new TooManyLoginAttemptsException($"Превышено количество попыток входа: {attempts}. Максимум: 3.");
        }

        Console.WriteLine($"Попытка входа {attempts}");
    }
}

class Program
{
    static void Main()
    {
        LoginManager loginManager = new LoginManager();

        for (int i = 1; i <= 4; i++)
        {
            try
            {
                loginManager.AttemptLogin(i);
            }
            catch (TooManyLoginAttemptsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }
}