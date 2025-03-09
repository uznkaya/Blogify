using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task CreateBlogPostAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostAsync();
        Task UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost);
        Task DeleteBlogPostAsync(int blogPostId);
        Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId);
        Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync(int count);
    }
}
