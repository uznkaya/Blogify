using Blogify.Application.Interfaces;
using Blogify.Domain.Common;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blogify.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LikeService> _logger;

        public LikeService(IUnitOfWork unitOfWork, ILogger<LikeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> CreateLikeAsync(Like like)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == like.UserId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                await _unitOfWork.Likes.AddAsync(like);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully created like.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating like");
                return Result.Failure("An error occurred while creating like.");
            }
        }

        public async Task<Result> DeleteLikeAsync(int likeId)
        {
            try
            {
                var like = await _unitOfWork.Likes.FindAsync(l => l.Id == likeId && !l.IsDeleted);
                if (like == null)
                    return Result.Failure("Like not found");

                await _unitOfWork.Likes.DeleteLikeAsync(likeId);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully deleted like.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting like");
                return Result.Failure("An error occurred while deleting like.");
            }
        }

        public async Task<Result<IEnumerable<Like>>> GetAllLikesAsync()
        {
            try
            {
                var likes = await _unitOfWork.Likes.GetAllAsync();
                if (!likes.Any())
                    return Result.Failure<IEnumerable<Like>>("No likes found");

                return Result.Success(likes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching likes");
                return Result.Failure<IEnumerable<Like>>("An error occurred while fetching likes");
            }
        }

        public async Task<Result<Like>> GetLikeByIdAsync(int likeId)
        {
            try
            {
                var like = await _unitOfWork.Likes.FindAsync(l => l.Id == likeId && !l.IsDeleted);
                if (like == null)
                    return Result.Failure<Like>("Like not found");

                return Result.Success(like);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching like");
                return Result.Failure<Like>("An error occurred while fetching like");
            }
        }
        public async Task<Result<int>> GetLikeCountByBlogPostIdAsync(int blogPostId)
        {
            try
            {
                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure<int>("Blog post not found");

                var likeCount = await _unitOfWork.Likes.GetLikeCountByBlogPostIdAsync(blogPost.Id);

                return Result.Success(likeCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching like count");
                return Result.Failure<int>("An error occurred while fetching like count");
            }
        }

        public async Task<Result<bool>> IsLikedByUserAsync(int userId, int blogPostId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure<bool>("User not found");

                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure<bool>("Blogpost not found");

                return Result.Success(await _unitOfWork.Likes.IsLikedByUserAsync(user.Id, blogPost.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if user liked blogpost");
                return Result.Failure<bool>("An error occurred while checking if user liked blogpost");
            }
        }
    }
}
