using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Domain.Exceptions
{
    public class BlogPostNotFoundException:Exception
    {
        public BlogPostNotFoundException() : base("Blog post not found.") { }
        public BlogPostNotFoundException(string message) : base(message) { }
        public BlogPostNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
