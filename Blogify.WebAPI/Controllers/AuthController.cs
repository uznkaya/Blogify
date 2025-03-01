using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blogify.Application.Interfaces;
using Blogify.Application.DTOs;

namespace Blogify.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            await _authService.Register(model.Name, model.Surname, model.Username, model.Password);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.Authenticate(model.Username, model.Password);
            return Ok(new { Token = token });
        }
    }
}
