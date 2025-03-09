using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Repositories
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task DeleteLikeAsync(int likeId)
        {
            var like = await _dbSet
                .FirstOrDefaultAsync(l => l.Id == likeId && !l.IsDeleted);

            like.IsDeleted = true;
        }

        public async Task<int> GetLikeCountByBlogPostIdAsync(int blogPostId)
        {
            return await _dbSet
                .Where(l => l.BlogPostId == blogPostId && !l.IsDeleted)
                .CountAsync();
        }

        public async Task<bool> IsLikedByUserAsync(int userId, int blogPostId)
        {
            return await _dbSet
                .AnyAsync(l => l.UserId == userId && l.BlogPostId == blogPostId && !l.IsDeleted);
        }
    }
}
