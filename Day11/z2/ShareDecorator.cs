class ShareDecorator : PostDecorator
{
    private int _shares = 0;

    public ShareDecorator(IPost post) : base(post) { }

    public void AddShare()
    {
        _shares++;
    }

    public override string GetContent()
    {
        return _post.GetContent() + $"\nРепосты: {_shares}";
    }
}
