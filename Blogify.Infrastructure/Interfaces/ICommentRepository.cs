using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task DeleteCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsByBlogPostIdAsync(int blogPostId);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);
        Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId);
    }
}
