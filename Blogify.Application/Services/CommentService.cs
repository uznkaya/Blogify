using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Domain.Exceptions;
using Blogify.Infrastructure.Interfaces;

namespace Blogify.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == comment.UserId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == comment.BlogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = _unitOfWork.Users.FindAsync(u => u.Id == commentId && !u.IsDeleted);
            if (comment == null)
                throw new CommentNotFoundException();

            await _unitOfWork.Comments.DeleteCommentAsync(commentId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync();
            if (!comments.Any())
                throw new CommentNotFoundException();

            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            var comment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
            if (comment == null)
                throw new CommentNotFoundException();

            return comment;
        }

        public async Task UpdateCommentAsync(int commentId, Comment updatedComment)
        {
            var comment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
            if (comment == null)
                throw new CommentNotFoundException();

            comment.Content = updatedComment.Content;

            await _unitOfWork.Comments.UpdateAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBlogPostIdAsync(int blogPostId)
        {
            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            var comments = await _unitOfWork.Comments.GetCommentsByBlogPostIdAsync(blogPost.Id);
            if (!comments.Any())
                throw new CommentNotFoundException();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var comments = await _unitOfWork.Comments.GetCommentsByUserIdAsync(user.Id);
            if (!comments.Any())
                throw new CommentNotFoundException();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetRepliesByCommentIdAsync(int commentId)
        {
            var parentComment = await _unitOfWork.Comments.FindAsync(c => c.Id == commentId && !c.IsDeleted);
            if (parentComment == null)
                throw new CommentNotFoundException("Parent Comment not found.");

            var replies = await _unitOfWork.Comments.GetRepliesByCommentIdAsync(parentComment.Id);
            if (!replies.Any())
                throw new CommentNotFoundException($"Replies not found for parentCommandId:{commentId}");

            return replies;
        }
    }
}
