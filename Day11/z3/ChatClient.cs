class ChatClient
{
    public void SendCommand(ICommand command)
    {
        command.Execute();
    }
}
