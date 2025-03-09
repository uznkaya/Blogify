using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .Where(u => u.Username == username && !u.IsDeleted)
                .FirstOrDefaultAsync();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _dbSet
                .Include(bp => bp.BlogPosts)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

            user.IsDeleted = true;

            foreach (var blogPost in user.BlogPosts)
                blogPost.IsDeleted = true;

            foreach (var comment in user.Comments)
                comment.IsDeleted = true;

            foreach (var like in user.Likes)
                like.IsDeleted = true;
        }
        public async Task<IEnumerable<BlogPost>> GetUserBlogPostsAsync(int userId)
        {
            return await _dbSet
                .Where(u => u.Id == userId && !u.IsDeleted)
                .SelectMany(u => u.BlogPosts)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetUserCommentsAsync(int userId)
        {
            return await _dbSet
                .Where(u => u.Id == userId && !u.IsDeleted)
                .SelectMany(u => u.Comments)
                .ToListAsync();
        }
        public async Task<IEnumerable<Like>> GetUserLikesAsync(int userId)
        {
            return await _dbSet
                .Where(u => u.Id == userId && !u.IsDeleted)
                .SelectMany(u => u.Likes)
                .ToListAsync();
        }
    }
}
