using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Application.DTOs
{
    public class LikeDto
    {
        public int UserId { get; set; }
        public int? BlogPostId { get; set; }
        public int? CommentId { get; set; }
    }
}
