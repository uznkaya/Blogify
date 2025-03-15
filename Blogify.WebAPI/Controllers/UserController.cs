using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUserAsync();
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Username = userDto.Username,
                PasswordHash = userDto.Password
            };

            var result = await _userService.UpdateUserAsync(userId, user);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var result = await _userService.GetUserByUsernameAsync(username);
            return Ok(result);
        }

        [HttpGet("{userId}/blogposts")]
        public async Task<IActionResult> GetUserBlogPosts(int userId)
        {
            var result = await _userService.GetUserBlogPostsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{userId}/comments")]
        public async Task<IActionResult> GetUserComments(int userId)
        {
            var result = await _userService.GetUserCommentsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{userId}/likes")]
        public async Task<IActionResult> GetUserLikes(int userId)
        {
            var result = await _userService.GetUserLikesAsync(userId);
            return Ok(result);
        }
    }
}
