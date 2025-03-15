using Blogify.Application.Interfaces;
using Blogify.Domain.Common;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blogify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<User>>> GetAllUserAsync()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                if (users == null)
                    return Result.Failure<IEnumerable<User>>("Users not found");

                return Result.Success(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users");
                return Result.Failure<IEnumerable<User>>("An error occurred while getting all users");
            }
        }
        public async Task<Result> UpdateUserAsync(int userId, User updatedUser)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Username = updatedUser.Username;

                if (!string.IsNullOrEmpty(updatedUser.PasswordHash) && !BCrypt.Net.BCrypt.Verify(updatedUser.PasswordHash, user.PasswordHash))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
                }

                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully updated user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user");
                return Result.Failure("An error occurred while updating user.");
            }
        }
        public async Task<Result> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                await _unitOfWork.Users.DeleteUserAsync(user.Id);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully deleted user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user");
                return Result.Failure("An error occurred while deleting user.");
            }
        }
        public async Task<Result<User>> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByUsernameAsync(username);
                if (user == null)
                    return Result.Failure<User>("User not found");

                return Result.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user by username");
                return Result.Failure<User>("An error occurred while getting user by username");
            }
        }
        public async Task<Result<IEnumerable<BlogPost>>> GetUserBlogPostsAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure<IEnumerable<BlogPost>>("User not found");

                var blogposts = await _unitOfWork.Users.GetUserBlogPostsAsync(user.Id);
                if (!blogposts.Any())
                    return Result.Failure<IEnumerable<BlogPost>>("No blogposts found for this user");

                return Result.Success(blogposts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching blogposts");
                return Result.Failure<IEnumerable<BlogPost>>("An error occurred while fetching blogposts");
            }
        }
        public async Task<Result<IEnumerable<Comment>>> GetUserCommentsAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure<IEnumerable<Comment>>("User not found");

                var comments = await _unitOfWork.Users.GetUserCommentsAsync(user.Id);
                if (!comments.Any())
                    return Result.Failure<IEnumerable<Comment>>("No comments found for this user");

                return Result.Success(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching comments");
                return Result.Failure<IEnumerable<Comment>>("An error occurred while fetching comments");
            }
        }
        public async Task<Result<IEnumerable<Like>>> GetUserLikesAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure<IEnumerable<Like>>("User not found");
                
                var likes = await _unitOfWork.Users.GetUserLikesAsync(user.Id);
                if (!likes.Any())
                    return Result.Failure<IEnumerable<Like>>("No likes found for this user");

                return Result.Success(likes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching likes");
                return Result.Failure<IEnumerable<Like>>("An error occurred while fetching likes");
            }
        }
    }
}
