using Blogify.Domain.Entities;
using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                throw new Exception("Comment not found");

            comment.IsDeleted = true;

            foreach (var reply in comment.Replies)
            {
                reply.IsDeleted = true;
            }

            foreach (var like in comment.Likes)
            {
                like.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
