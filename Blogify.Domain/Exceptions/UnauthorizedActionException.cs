using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Domain.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException() : base("Unauthorized action.") { }
        public UnauthorizedActionException(string message) : base(message) { }
    }
}
