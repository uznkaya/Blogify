using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Domain.Exceptions;
using Blogify.Infrastructure.Interfaces;

namespace Blogify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            if (users == null)
                throw new UserNotFoundException();

            return users;
        }
        public async Task UpdateUserAsync(int userId, User updatedUser)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Username = updatedUser.Username;

            if (!string.IsNullOrEmpty(updatedUser.PasswordHash) && !BCrypt.Net.BCrypt.Verify(updatedUser.PasswordHash, user.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
            }

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            await _unitOfWork.Users.DeleteUserAsync(user.Id);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            if (user == null)
                throw new UserNotFoundException();

            return user;
        }
        public async Task<IEnumerable<BlogPost>> GetUserBlogPostsAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var blogposts = await _unitOfWork.Users.GetUserBlogPostsAsync(user.Id);
            if (!blogposts.Any())
                throw new BlogPostNotFoundException();

            return blogposts;
        }
        public async Task<IEnumerable<Comment>> GetUserCommentsAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var comments = await _unitOfWork.Users.GetUserCommentsAsync(user.Id);
            if (!comments.Any())
                throw new CommentNotFoundException();

            return comments;
        }
        public async Task<IEnumerable<Like>> GetUserLikesAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var likes = await _unitOfWork.Users.GetUserLikesAsync(user.Id);
            if (!likes.Any())
                throw new LikeNotFoundException();

            return likes;
        }
    }
}
