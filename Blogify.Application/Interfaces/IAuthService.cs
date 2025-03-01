namespace Blogify.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
        Task Register(string name, string surname, string username, string password);
    }
}
