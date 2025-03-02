using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly BlogifyDbContext _context;
        public UserRepository(BlogifyDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(bp => bp.BlogPosts)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(x => x.Id == userId);

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

        public async Task EditUserAsync(User user)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (_user == null)
                throw new Exception("User not found");

            _context.Users.Update(_user);
            await _context.SaveChangesAsync();
        }
    }
}
