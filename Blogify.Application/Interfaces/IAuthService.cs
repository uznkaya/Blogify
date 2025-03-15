using Blogify.Application.DTOs;
using Blogify.Domain.Common;

namespace Blogify.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> Authenticate(LoginModel loginModel);
        Task<Result> Register(RegisterModel registerModel);
    }
}
