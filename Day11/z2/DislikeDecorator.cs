class DislikeDecorator : PostDecorator
{
    private int _dislikes = 0;

    public DislikeDecorator(IPost post) : base(post) { }

    public void AddDislike()
    {
        _dislikes++;
    }

    public override string GetContent()
    {
        return _post.GetContent() + $"\nДизлайки: {_dislikes}";
    }
}
