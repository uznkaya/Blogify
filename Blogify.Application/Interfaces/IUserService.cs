using Blogify.Domain.Common;
using Blogify.Domain.Entities;

namespace Blogify.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<User>>> GetAllUserAsync();
        Task<Result> UpdateUserAsync(int userId, User updatedUser);
        Task<Result> DeleteUserAsync(int userId);
        Task<Result<User>> GetUserByUsernameAsync(string username);
        Task<Result<IEnumerable<BlogPost>>> GetUserBlogPostsAsync(int userId);
        Task<Result<IEnumerable<Comment>>> GetUserCommentsAsync(int userId);
        Task<Result<IEnumerable<Like>>> GetUserLikesAsync(int userId);
    }
}
