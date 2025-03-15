using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Domain.Exceptions
{
    public class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException() : base("Comment not found.") { }
        public CommentNotFoundException(string message) : base(message) { }
    }
}
