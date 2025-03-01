namespace Blogify.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
