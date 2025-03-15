using Blogify.Domain.Common;
using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface ILikeService
    {
        Task<Result> CreateLikeAsync(Like blogPost);
        Task<Result<IEnumerable<Like>>> GetAllLikesAsync();
        Task<Result> DeleteLikeAsync(int likeId);
        Task<Result<Like>> GetLikeByIdAsync(int likeId);
        Task<Result<int>> GetLikeCountByBlogPostIdAsync(int blogPostId);
        Task<Result<bool>> IsLikedByUserAsync(int userId, int blogPostId);
    }
}
