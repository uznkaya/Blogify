using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Domain.Exceptions;
using Blogify.Infrastructure.Interfaces;

namespace Blogify.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateLikeAsync(Like like)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == like.UserId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteLikeAsync(int likeId)
        {
            var like = await _unitOfWork.Likes.FindAsync(l => l.Id == likeId && !l.IsDeleted);
            if (like == null)
                throw new LikeNotFoundException();

            await _unitOfWork.Likes.DeleteLikeAsync(likeId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Like>> GetAllLikesAsync()
        {
            var likes = await _unitOfWork.Likes.GetAllAsync();
            if (!likes.Any())
                throw new LikeNotFoundException();

            return likes;
        }

        public async Task<Like> GetLikeByIdAsync(int likeId)
        {
            var like = await _unitOfWork.Likes.FindAsync(l => l.Id == likeId && !l.IsDeleted);
            if (like == null)
                throw new LikeNotFoundException();

            return like;
        }
        public async Task<int> GetLikeCountByBlogPostIdAsync(int blogPostId)
        {
            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            return await _unitOfWork.Likes.GetLikeCountByBlogPostIdAsync(blogPost.Id);
        }

        public async Task<bool> IsLikedByUserAsync(int userId, int blogPostId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            return await _unitOfWork.Likes.IsLikedByUserAsync(user.Id, blogPost.Id);
        }
    }
}
