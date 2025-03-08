using Blogify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Interfaces
{
    public interface ILikeService
    {
        Task CreateLikeAsync(Like blogPost);
        Task<IEnumerable<Like>> GetAllLikesAsync();
        Task DeleteLikeAsync(int likeId);
        Task<Like> GetLikeByIdAsync(int likeId);
    }
}
