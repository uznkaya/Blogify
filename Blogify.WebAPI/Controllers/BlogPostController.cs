using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogPost(BlogPostDto blogPostDto)
        {
            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                UserId = blogPostDto.UserId
            };

            var result = await _blogPostService.CreateBlogPostAsync(blogPost);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var result = await _blogPostService.GetAllBlogPostAsync();
            return Ok(result);
        }

        [HttpPut("{blogPostId}")]
        public async Task<IActionResult> EditBlogPost(int blogPostId, BlogPostDto blogPostDto)
        {
            var editedBlogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                UserId = blogPostDto.UserId
            };

            var result = await _blogPostService.UpdateBlogPostAsync(blogPostId, editedBlogPost);
            return Ok(result);
        }

        [HttpDelete("{blogPostId}")]
        public async Task<IActionResult> DeleteBlogPost(int blogPostId)
        {
            var result = await _blogPostService.DeleteBlogPostAsync(blogPostId);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBlogPostsByUserId(int userId)
        {
            var result = await _blogPostService.GetBlogPostsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("recent/{count}")]
        public async Task<IActionResult> GetRecentBlogPosts(int count)
        {
            var result = await _blogPostService.GetRecentBlogPostsAsync(count);
            return Ok(result);
        }
    }
}
