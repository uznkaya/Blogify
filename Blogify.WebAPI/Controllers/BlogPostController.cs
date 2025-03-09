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
            try
            {
                var blogPost = new BlogPost
                {
                    Title = blogPostDto.Title,
                    Content = blogPostDto.Content,
                    UserId = blogPostDto.UserId
                };

                await _blogPostService.CreateBlogPostAsync(blogPost);
                return Ok("Blogpost successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            try
            {
                var blogPosts = await _blogPostService.GetAllBlogPostAsync();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{blogPostId}")]
        public async Task<IActionResult> EditBlogPost(int blogPostId, BlogPostDto blogPostDto)
        {
            try
            {
                var editedBlogPost = new BlogPost
                {
                    Title = blogPostDto.Title,
                    Content = blogPostDto.Content,
                    UserId = blogPostDto.UserId
                };

                await _blogPostService.UpdateBlogPostAsync(blogPostId, editedBlogPost);
                return Ok("Blogpost successfully edited.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{blogPostId}")]
        public async Task<IActionResult> DeleteBlogPost(int blogPostId)
        {
            try
            {
                await _blogPostService.DeleteBlogPostAsync(blogPostId);
                return Ok(new { Message = "Blogpost successfully deleted " });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBlogPostsByUserId(int userId)
        {
            try
            {
                var blogPosts = await _blogPostService.GetBlogPostsByUserIdAsync(userId);
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("recent/{count}")]
        public async Task<IActionResult> GetRecentBlogPosts(int count)
        {
            try
            {
                var blogPosts = await _blogPostService.GetRecentBlogPostsAsync(count);
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
