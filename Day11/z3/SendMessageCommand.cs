class SendMessageCommand : ICommand
{
    private ChatService _chatService;
    private string _message;

    public SendMessageCommand(ChatService chatService, string message)
    {
        _chatService = chatService;
        _message = message;
    }

    public void Execute()
    {
        _chatService.SendMessage(_message);
    }
}
