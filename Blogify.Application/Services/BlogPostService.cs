using Blogify.Application.Interfaces;
using Blogify.Domain.Common;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blogify.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogPostService> _logger;

        public BlogPostService(IUnitOfWork unitOfWork, ILogger<BlogPostService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> CreateBlogPostAsync(BlogPost blogPost)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == blogPost.UserId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                await _unitOfWork.BlogPosts.AddAsync(blogPost);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully created blogpost.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating blogpost");
                return Result.Failure("An error occurred while creating blogpost.");
            }
        }

        public async Task<Result<IEnumerable<BlogPost>>> GetAllBlogPostAsync()
        {
            try
            {
                var blogposts = await _unitOfWork.BlogPosts.GetAllAsync();
                if (!blogposts.Any())
                    return Result.Failure<IEnumerable<BlogPost>>("No blogposts found");

                return Result.Success(blogposts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching blogposts");
                return Result.Failure<IEnumerable<BlogPost>>("An error occurred while fetching blogposts");
            }
        }

        public async Task<Result> UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == updatedBlogPost.UserId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure("Blogpost not found");

                blogPost.Title = updatedBlogPost.Title;
                blogPost.Content = updatedBlogPost.Content;
                blogPost.UserId = updatedBlogPost.UserId;

                await _unitOfWork.BlogPosts.UpdateAsync(blogPost);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully updated blogpost.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating blogpost");
                return Result.Failure("An error occurred while updating blogpost.");
            }
        }
        public async Task<Result> DeleteBlogPostAsync(int blogPostId)
        {
            try
            {
                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure("Blogpost not found");

                await _unitOfWork.BlogPosts.DeleteBlogPostAsync(blogPostId);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully deleted blogpost.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting blogpost");
                return Result.Failure("An error occurred while deleting blogpost.");
            }
        }

        public async Task<Result<BlogPost>> GetBlogPostByIdAsync(int blogPostId)
        {
            try
            {
                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure<BlogPost>("Blogpost not found");

                return Result.Success(blogPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching blogpost");
                return Result.Failure<BlogPost>("An error occurred while fetching blogpost");

            }
        }

        public async Task<Result<IEnumerable<BlogPost>>> GetBlogPostsByUserIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(bp => bp.Id == userId && !bp.IsDeleted);
                if (user == null)
                    return Result.Failure<IEnumerable<BlogPost>>("User not found");

                var blogPosts = await _unitOfWork.BlogPosts.GetBlogPostsByUserIdAsync(user.Id);
                if (!blogPosts.Any())
                    return Result.Failure<IEnumerable<BlogPost>>("No blogposts found");

                return Result.Success(blogPosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching blogposts");
                return Result.Failure<IEnumerable<BlogPost>>("An error occurred while fetching blogposts");
            }
        }

        public async Task<Result<IEnumerable<BlogPost>>> GetRecentBlogPostsAsync(int count)
        {
            try
            {
                var blogPosts = await _unitOfWork.BlogPosts.GetRecentBlogPostsAsync(count);
                if (!blogPosts.Any())
                    return Result.Failure<IEnumerable<BlogPost>>("No blogposts found");

                return Result.Success(blogPosts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching blogposts");
                return Result.Failure<IEnumerable<BlogPost>>("An error occurred while fetching blogposts");
            }
        }
    }
}
