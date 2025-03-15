using Blogify.Domain.Common;
using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<Result> CreateBlogPostAsync(BlogPost blogPost);
        Task<Result<IEnumerable<BlogPost>>> GetAllBlogPostAsync();
        Task<Result> UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost);
        Task<Result> DeleteBlogPostAsync(int blogPostId);
        Task<Result<IEnumerable<BlogPost>>> GetBlogPostsByUserIdAsync(int userId);
        Task<Result<IEnumerable<BlogPost>>> GetRecentBlogPostsAsync(int count);
    }
}
