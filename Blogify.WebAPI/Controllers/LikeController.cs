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
            var like = new Like
            {
                UserId = likeDto.UserId,
                BlogPostId = likeDto.BlogPostId,
                CommentId = likeDto.CommentId
            };

            var result = await _likeService.CreateLikeAsync(like);
            return Ok(result);
        }

        [HttpDelete("{likeId}")]
        public async Task<IActionResult> DeleteLike(int likeId)
        {
            var result = await _likeService.DeleteLikeAsync(likeId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLikes()
        {
            var result = await _likeService.GetAllLikesAsync();
            return Ok(result);
        }

        [HttpGet("{likeId}")]
        public async Task<IActionResult> GetLikeById(int likeId)
        {
            var result = await _likeService.GetLikeByIdAsync(likeId);
            return Ok(result);
        }

        [HttpGet("count/{blogPostId}")]
        public async Task<IActionResult> GetLikeCountByBlogPostId(int blogPostId)
        {
            var result = await _likeService.GetLikeCountByBlogPostIdAsync(blogPostId);
            return Ok(result);
        }

        [HttpGet("isliked/{userId}/{blogPostId}")]
        public async Task<IActionResult> IsLikedByUser(int userId, int blogPostId)
        {
            var result = await _likeService.IsLikedByUserAsync(userId, blogPostId);
            return Ok(result);
        }
    }
}
