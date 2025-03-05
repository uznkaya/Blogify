using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task DeleteUserAsync(int userId);
    }
}
