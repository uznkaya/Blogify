namespace Blogify.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int? BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        
        public int? CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
