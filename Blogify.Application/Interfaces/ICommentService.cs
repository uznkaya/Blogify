using Blogify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task UpdateCommentAsync(int commentId, Comment comment);
        Task DeleteCommentAsync(int commentId);
        Task<Comment> GetCommentByIdAsync(int commentId);
    }
}
