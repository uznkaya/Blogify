using Blogify.Domain.Common;
using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface ICommentService
    {
        Task<Result> CreateCommentAsync(Comment comment);
        Task<Result<IEnumerable<Comment>>> GetAllCommentsAsync();
        Task<Result> UpdateCommentAsync(int commentId, Comment comment);
        Task<Result> DeleteCommentAsync(int commentId);
        Task<Result<Comment>> GetCommentByIdAsync(int commentId);
        Task<Result<IEnumerable<Comment>>> GetCommentsByBlogPostIdAsync(int blogPostId);
        Task<Result<IEnumerable<Comment>>> GetCommentsByUserIdAsync(int userId);
        Task<Result<IEnumerable<Comment>>> GetRepliesByCommentIdAsync(int commentId);
    }
}
