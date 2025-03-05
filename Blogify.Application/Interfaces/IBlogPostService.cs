using Blogify.Application.DTOs;
using Blogify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task CreateBlogPostAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostAsync();
        Task UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost);
        Task DeleteBlogPostAsync(int blogPostId);
    }
}
