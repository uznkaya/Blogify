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
                .Include(bp => bp.BlogPosts)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _dbSet
                .Include(bp => bp.BlogPosts)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            user.IsDeleted = true;

            foreach (var blogPost in user.BlogPosts)
            {
                blogPost.IsDeleted = true;
            }

            foreach (var comment in user.Comments)
            {
                comment.IsDeleted = true;
            }

            foreach (var like in user.Likes)
            {
                like.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
