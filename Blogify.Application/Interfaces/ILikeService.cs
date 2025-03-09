using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface ILikeService
    {
        Task CreateLikeAsync(Like blogPost);
        Task<IEnumerable<Like>> GetAllLikesAsync();
        Task DeleteLikeAsync(int likeId);
        Task<Like> GetLikeByIdAsync(int likeId);
        Task<int> GetLikeCountByBlogPostIdAsync(int blogPostId);
        Task<bool> IsLikedByUserAsync(int userId, int blogPostId);
    }
}
