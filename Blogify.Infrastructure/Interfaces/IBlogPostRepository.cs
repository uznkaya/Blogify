using Blogify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Task DeleteBlogPostAsync(int blogPostId);
    }
}
