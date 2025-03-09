namespace Blogify.Infrastructure.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        IBlogPostRepository BlogPosts { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
        Task<int> CompleteAsync();
    }
}
