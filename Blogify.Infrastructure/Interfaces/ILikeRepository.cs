using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task DeleteLikeAsync(int likeId);
        Task<int> GetLikeCountByBlogPostIdAsync(int blogPostId);
        Task<bool> IsLikedByUserAsync(int userId, int blogPostId);
    }
}
