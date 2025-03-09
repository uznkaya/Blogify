using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task UpdateCommentAsync(int commentId, Comment comment);
        Task DeleteCommentAsync(int commentId);
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsByBlogPostIdAsync(int blogPostId);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);
        Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId);
    }
}
