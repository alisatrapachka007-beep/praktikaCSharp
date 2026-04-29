using System;

class Program
{
    static void Main()
    {
        ChatService receiver = new ChatService();
        ICommand command = new SendMessageCommand(receiver, "привет, мир");
        ChatClient invoker = new ChatClient();

        invoker.SendCommand(command);
    }
}