using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Domain.Exceptions
{
    public class BlogPostNotFoundException: NotFoundException
    {
        public BlogPostNotFoundException() : base("Blog post not found.") { }
        public BlogPostNotFoundException(string message) : base(message) { }
    }
}
