class BasicPost : IPost
{
    private string _content;

    public BasicPost(string content)
    {
        _content = content;
    }

    public string GetContent() => _content;
}
