using Blogify.Application.Interfaces;
using Blogify.Application.DTOs;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
            try
            {
                var comment = new Comment
                {
                    Content = commentDto.Content,
                    UserId = commentDto.UserId,
                    BlogPostId = commentDto.BlogPostId,
                    ParentCommentId = commentDto.ParentCommentId
                };
                await _commentService.CreateCommentAsync(comment);
                return Ok("Comment successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId);
                return Ok("Comment successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(commentId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(int commentId, CommentDto commentDto)
        {
            try
            {
                var comment = new Comment
                {
                    Content = commentDto.Content,
                    UserId = commentDto.UserId,
                    BlogPostId = commentDto.BlogPostId,
                    ParentCommentId = commentDto.ParentCommentId
                };

                await _commentService.UpdateCommentAsync(commentId, comment);
                return Ok("Comment successfully updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
