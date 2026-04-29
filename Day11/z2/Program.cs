using System;

class Program
{
    static void Main()
    {
        IPost post = new BasicPost("Первый пост");

        LikeDecorator liked = new LikeDecorator(post);
        liked.AddLike();
        liked.AddLike();

        ShareDecorator shared = new ShareDecorator(liked);
        shared.AddShare();

        Console.WriteLine(shared.GetContent());
    }
}