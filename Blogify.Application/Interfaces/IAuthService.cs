using Blogify.Application.DTOs;

namespace Blogify.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(LoginModel loginModel);
        Task Register(RegisterModel registerModel);
    }
}
