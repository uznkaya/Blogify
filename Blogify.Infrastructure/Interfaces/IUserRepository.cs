using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task DeleteUserAsync(int userId);
        Task<IEnumerable<BlogPost>> GetUserBlogPostsAsync(int userId);
        Task<IEnumerable<Comment>> GetUserCommentsAsync(int userId);
        Task<IEnumerable<Like>> GetUserLikesAsync(int userId);
    }
}
