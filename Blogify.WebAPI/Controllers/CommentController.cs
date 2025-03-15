using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                BlogPostId = commentDto.BlogPostId,
                ParentCommentId = commentDto.ParentCommentId
            };
            var result = await _commentService.CreateCommentAsync(comment);
            return Ok(result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _commentService.GetAllCommentsAsync();
            return Ok(result);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var result = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(result);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, CommentDto commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                BlogPostId = commentDto.BlogPostId,
                ParentCommentId = commentDto.ParentCommentId
            };

            var result = await _commentService.UpdateCommentAsync(commentId, comment);
            return Ok(result);
        }

        [HttpGet("blogpost/{blogPostId}")]
        public async Task<IActionResult> GetCommentsByBlogPostId(int blogPostId)
        {
            var result = await _commentService.GetCommentsByBlogPostIdAsync(blogPostId);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId(int userId)
        {
            var result = await _commentService.GetCommentsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("replies/{commentId}")]
        public async Task<IActionResult> GetRepliesByCommentId(int commentId)
        {
            var result = await _commentService.GetRepliesByCommentIdAsync(commentId);
            return Ok(result);
        }
    }
}
