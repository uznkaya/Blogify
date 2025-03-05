using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository, IUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
        }

        public async Task CreateBlogPostAsync(BlogPost blogPost)
        {
            var user = await _userRepository.GetByIdAsync(blogPost.UserId);
            if (user == null)
                throw new Exception("User not found");

            await _blogPostRepository.AddAsync(blogPost);
        }
        public async Task<IEnumerable<BlogPost>> GetAllBlogPostAsync()
        {
            var blogposts = await _blogPostRepository.GetAllAsync();
            if (blogposts == null) 
                throw new Exception("No blogposts found");
            
            return blogposts;
        }
        public async Task UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost)
        {
            var user = await _userRepository.GetByIdAsync(updatedBlogPost.UserId);
            if (user == null)
                throw new Exception("User not found");

            var blogPost = await _blogPostRepository.GetByIdAsync(blogPostId);
            if (blogPost == null)
                throw new Exception("Blogpost not found");

            blogPost.Title = updatedBlogPost.Title;
            blogPost.Content = updatedBlogPost.Content;
            blogPost.UserId = updatedBlogPost.UserId;

            await _blogPostRepository.UpdateAsync(blogPost);
        }
        public async Task DeleteBlogPostAsync(int blogPostId)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(blogPostId);
            if (blogPost == null)
                throw new Exception("Blogpost not found");

            await _blogPostRepository.DeleteBlogPostAsync(blogPostId);
        }
        public async Task<BlogPost> GetBlogPostByIdAsync(int blogPostId)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(blogPostId);
            if (blogPost == null)
                throw new Exception("Blogpost not found");

            return blogPost;
        }
    }
}
