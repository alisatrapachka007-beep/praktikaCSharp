class LikeDecorator : PostDecorator
{
    private int _likes = 0;

    public LikeDecorator(IPost post) : base(post) { }

    public void AddLike()
    {
        _likes++;
    }

    public override string GetContent()
    {
        return _post.GetContent() + $"\nЛайки: {_likes}";
    }
}
