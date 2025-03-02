﻿using Blogify.Domain.Entities;

namespace Blogify.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task EditUserAsync(User user);
    }
}
