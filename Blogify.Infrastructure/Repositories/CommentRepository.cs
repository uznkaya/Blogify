using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _dbSet
                    .Include(r => r.Replies)
                    .Include(l => l.Likes)
                    .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);

            comment.IsDeleted = true;

            foreach (var reply in comment.Replies)
            {
                reply.IsDeleted = true;
            }

            foreach (var like in comment.Likes)
            {
                like.IsDeleted = true;
            }
        }
        public async Task<IEnumerable<Comment>> GetCommentsByBlogPostIdAsync(int blogPostId)
        {
            return await _dbSet
                .Where(c => c.BlogPostId == blogPostId && !c.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await _dbSet
                .Where(c => c.ParentCommentId == commentId && !c.IsDeleted)
                .ToListAsync();
        }
    }
}
