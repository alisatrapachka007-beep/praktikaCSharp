abstract class PostDecorator : IPost
{
    protected IPost _post;

    public PostDecorator(IPost post)
    {
        _post = post;
    }

    public abstract string GetContent();
}
