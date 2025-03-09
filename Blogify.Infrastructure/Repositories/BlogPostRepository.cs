using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Repositories
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task DeleteBlogPostAsync(int blogPostId)
        {
            var _blogpost = await _dbSet
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);

            _blogpost.IsDeleted = true;

            foreach (var comment in _blogpost.Comments)
            {
                comment.IsDeleted = true;
            }
            foreach (var like in _blogpost.Likes)
            {
                like.IsDeleted = true;
            }
        }
        public async Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(bp => bp.UserId == userId && !bp.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync(int count)
        {
            return await _dbSet
                .Where(bp => !bp.IsDeleted)
                .OrderByDescending(bp => bp.CreatedDate)
                .Take(count)
                .ToListAsync();
        }
    }
}
