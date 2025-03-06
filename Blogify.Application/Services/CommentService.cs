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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IBlogPostRepository blogPostRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _blogPostRepository = blogPostRepository;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            var user = await _userRepository.GetByIdAsync(comment.UserId);
            if (user == null)
                throw new Exception("User not found");

            var blogPost = await _blogPostRepository.GetByIdAsync(comment.BlogPostId);
            if (blogPost == null)
                throw new Exception("Blogpost not found");

            await _commentRepository.AddAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = _commentRepository.GetByIdAsync(commentId);
            if(comment == null)
                throw new Exception("Comment not found");

            await _commentRepository.DeleteCommentAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            if (comments == null)
                throw new Exception("No comments found");

            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Comment not found");

            return comment;
        }

        public async Task UpdateCommentAsync(int commentId, Comment updatedComment)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new Exception("Comment not found");

            comment.Content = updatedComment.Content;

            await _commentRepository.UpdateAsync(comment);
        }
    }
}
