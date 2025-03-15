using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Common;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogify.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Result<string>> Authenticate(LoginModel loginModel)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByUsernameAsync(loginModel.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
                    return Result.Failure<string>("Invalid username or password.");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Result.Success(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating user.");
                return Result.Failure<string>("An error occurred while authenticating user.");
            }
        }

        public async Task<Result> Register(RegisterModel registerModel)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);
                var user = new User
                {
                    Username = registerModel.Username,
                    PasswordHash = hashedPassword,
                    Name = registerModel.Name,
                    Surname = registerModel.Surname
                };
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully user registered.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering user.");
                return Result.Failure("An error occurred while registering user.");
            }

        }
    }
}
