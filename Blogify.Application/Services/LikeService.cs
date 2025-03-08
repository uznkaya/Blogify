using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Blogify.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;

        public LikeService(ILikeRepository likeRepository, IUserRepository userRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        public async Task CreateLikeAsync(Like like)
        {
            var user = _userRepository.GetByIdAsync(like.UserId);
            if (user == null)
                throw new Exception("User not found");

            await _likeRepository.AddAsync(like);
        }

        public async Task DeleteLikeAsync(int likeId)
        {
            var like = _likeRepository.GetByIdAsync(likeId);
            if (like == null)
                throw new Exception("Like not found");

            await _likeRepository.DeleteLikeAsync(likeId);
        }

        public async Task<IEnumerable<Like>> GetAllLikesAsync()
        {
            var likes = await _likeRepository.GetAllAsync();
            if (likes == null)
                throw new Exception("No likes found");

            return likes;
        }

        public async Task<Like> GetLikeByIdAsync(int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like == null)
                throw new Exception("Like not found");

            return like;
        }
    }
}
