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
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogifyDbContext context) : base(context)
        {
        }

        public async Task DeleteBlogPostAsync(int blogPostId)
        {
            var _blogpost = await _context.BlogPosts
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .FirstOrDefaultAsync(x => x.Id == blogPostId);

            if (_blogpost == null)
                throw new Exception("BlogPost not found");

            _blogpost.IsDeleted = true;

            foreach (var comment in _blogpost.Comments)
            {
                comment.IsDeleted = true;
            }
            foreach (var like in _blogpost.Likes)
            {
                like.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
