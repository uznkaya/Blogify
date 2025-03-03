using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }
        
        [HttpPut]
        public async Task<IActionResult> EditUser(User user)
        {
            await _userService.EditUserAsync(user);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }


        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
