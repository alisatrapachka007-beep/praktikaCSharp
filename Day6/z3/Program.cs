using System;

delegate void MessageHandler(string sender, string message);

class ChatApplication
{
    public event MessageHandler MessageReceived;

    public void SendMessage(string sender, string message)
    {
        MessageReceived?.Invoke(sender, message);
    }
}

class DesktopNotifier
{
    public void Show(string sender, string message)
    {
        Console.WriteLine($"Всплывающее окно: {sender}: {message}");
    }
}

class SoundAlert
{
    public void Beep(string sender, string message)
    {
        Console.WriteLine($"Звуковой сигнал от {sender}");
    }
}

class Program
{
    static void Main()
    {
        ChatApplication chat = new ChatApplication();

        DesktopNotifier notifier = new DesktopNotifier();
        SoundAlert sound = new SoundAlert();

        chat.MessageReceived += notifier.Show;
        chat.MessageReceived += sound.Beep;

        chat.SendMessage("Анна", "Привет");
    }
}