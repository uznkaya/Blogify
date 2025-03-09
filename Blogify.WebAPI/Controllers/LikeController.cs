using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLike(LikeDto likeDto)
        {
            try
            {
                var like = new Like
                {
                    UserId = likeDto.UserId,
                    BlogPostId = likeDto.BlogPostId,
                    CommentId = likeDto.CommentId
                };

                await _likeService.CreateLikeAsync(like);
                return Ok("Like successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{likeId}")]
        public async Task<IActionResult> DeleteLike(int likeId)
        {
            try
            {
                await _likeService.DeleteLikeAsync(likeId);
                return Ok("Like successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLikes()
        {
            try
            {
                var likes = await _likeService.GetAllLikesAsync();
                return Ok(likes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{likeId}")]
        public async Task<IActionResult> GetLikeById(int likeId)
        {
            try
            {
                var like = await _likeService.GetLikeByIdAsync(likeId);
                return Ok(like);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("count/{blogPostId}")]
        public async Task<IActionResult> GetLikeCountByBlogPostId(int blogPostId)
        {
            try
            {
                var likeCount = await _likeService.GetLikeCountByBlogPostIdAsync(blogPostId);
                return Ok(likeCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("isliked/{userId}/{blogPostId}")]
        public async Task<IActionResult> IsLikedByUser(int userId, int blogPostId)
        {
            try
            {
                var isLiked = await _likeService.IsLikedByUserAsync(userId, blogPostId);
                return Ok(isLiked);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
