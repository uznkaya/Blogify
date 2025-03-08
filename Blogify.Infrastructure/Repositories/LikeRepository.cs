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
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task DeleteLikeAsync(int likeId)
        {
            var like = await _dbSet
                .FirstOrDefaultAsync(l => l.Id == likeId);

            if (like == null)
                throw new Exception("Like not found");

            like.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
