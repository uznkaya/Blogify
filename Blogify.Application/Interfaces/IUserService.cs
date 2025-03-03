using Blogify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<List<User>> GetAllUserAsync();
        Task EditUserAsync(User user);
        Task DeleteUserAsync(int userId);


        Task<User> GetUserByUsernameAsync(string username);
    }
}
