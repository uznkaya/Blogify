using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Domain.Exceptions
{
    public class LikeNotFoundException : NotFoundException
    {
        public LikeNotFoundException() : base("Like not found.") { }
        public LikeNotFoundException(string message) : base(message) { }
    }
}
