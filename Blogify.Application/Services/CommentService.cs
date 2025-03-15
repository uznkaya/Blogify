using Blogify.Application.Interfaces;
using Blogify.Domain.Common;
using Blogify.Domain.Entities;
using Blogify.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blogify.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;
        public CommentService(IUnitOfWork unitOfWork, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> CreateCommentAsync(Comment comment)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == comment.UserId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure("User not found");

                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == comment.BlogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure("BlogPost not found");

                await _unitOfWork.Comments.AddAsync(comment);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully created comment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating comment");
                return Result.Failure("An error occurred while creating comment.");
            }
        }

        public async Task<Result> DeleteCommentAsync(int commentId)
        {
            try
            {
                var comment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
                if (comment == null)
                    return Result.Failure("Comment not found");

                await _unitOfWork.Comments.DeleteCommentAsync(commentId);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully deleted comment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting comment");
                return Result.Failure("An error occurred while deleting comment.");
            }
        }

        public async Task<Result<IEnumerable<Comment>>> GetAllCommentsAsync()
        {
            try
            {
                var comments = await _unitOfWork.Comments.GetAllAsync();
                if (!comments.Any())
                    return Result.Failure<IEnumerable<Comment>>("No comments found");

                return Result.Success<IEnumerable<Comment>>(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching comments");
                return Result.Failure<IEnumerable<Comment>>("An error occurred while fetching comments");
            }
        }

        public async Task<Result<Comment>> GetCommentByIdAsync(int commentId)
        {
            try
            {
                var comment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
                if (comment == null)
                    return Result.Failure<Comment>("Comment not found");

                return Result.Success(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching comment");
                return Result.Failure<Comment>("An error occurred while fetching comment");
            }
        }

        public async Task<Result> UpdateCommentAsync(int commentId, Comment updatedComment)
        {
            try
            {
                var comment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
                if (comment == null)
                    return Result.Failure("Comment not found");

                comment.Content = updatedComment.Content;

                await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.CompleteAsync();

                return Result.Success("Successfully updated comment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating comment");
                return Result.Failure("An error occurred while updating comment.");
            }
        }

        public async Task<Result<IEnumerable<Comment>>> GetCommentsByBlogPostIdAsync(int blogPostId)
        {
            try
            {
                var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
                if (blogPost == null)
                    return Result.Failure<IEnumerable<Comment>>("BlogPost not found");

                var comments = await _unitOfWork.Comments.GetCommentsByBlogPostIdAsync(blogPost.Id);
                if (!comments.Any())
                    return Result.Failure<IEnumerable<Comment>>("No comments found for this blogpost");

                return Result.Success(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching comments");
                return Result.Failure<IEnumerable<Comment>>("An error occurred while fetching comments");
            }
        }

        public async Task<Result<IEnumerable<Comment>>> GetCommentsByUserIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                    return Result.Failure<IEnumerable<Comment>>("User not found");

                var comments = await _unitOfWork.Comments.GetCommentsByUserIdAsync(user.Id);
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

        public async Task<Result<IEnumerable<Comment>>> GetRepliesByCommentIdAsync(int commentId)
        {
            try
            {
                var parentComment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
                if (parentComment == null)
                    return Result.Failure<IEnumerable<Comment>>("Parent Comment not found");

                var replies = await _unitOfWork.Comments.GetRepliesByCommentIdAsync(parentComment.Id);
                if (!replies.Any())
                    return Result.Failure<IEnumerable<Comment>>("Replies not found for parentCommandId");

                return Result.Success(replies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching replies");
                return Result.Failure<IEnumerable<Comment>>("An error occurred while fetching replies");
            }
        }
    }
}
