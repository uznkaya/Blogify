using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<List<User>> GetAllUserAsync();
        Task EditUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
