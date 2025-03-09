using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task UpdateUserAsync(int userId, User updatedUser);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<BlogPost>> GetUserBlogPostsAsync(int userId);
        Task<IEnumerable<Comment>> GetUserCommentsAsync(int userId);
        Task<IEnumerable<Like>> GetUserLikesAsync(int userId);
    }
}
