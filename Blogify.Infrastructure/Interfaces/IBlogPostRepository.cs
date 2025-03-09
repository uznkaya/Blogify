using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Task DeleteBlogPostAsync(int blogPostId);
        Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId);
        Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync(int count);
    }
}
