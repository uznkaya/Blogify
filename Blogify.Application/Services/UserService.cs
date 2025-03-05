using Blogify.Application.DTOs;
using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null)
                throw new Exception("No users found");

            return users;
        }
        public async Task UpdateUserAsync(User updatedUser, string? newPassword = null)
        {
            var user = await _userRepository.GetByIdAsync(updatedUser.Id);
            if (user == null)
                throw new Exception("User not found");

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Username = updatedUser.Username;

            if (!string.IsNullOrEmpty(newPassword))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteUserAsync(int userId)
        {
            var user = _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            await _userRepository.DeleteUserAsync(userId);
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }
    }
}
