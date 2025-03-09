using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;

namespace Blogify.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogifyDbContext _blogifyDbContext;

        public IUserRepository Users { get; private set; }
        public IBlogPostRepository BlogPosts { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ILikeRepository Likes { get; private set; }

        public UnitOfWork(BlogifyDbContext blogifyDbContext)
        {
            _blogifyDbContext = blogifyDbContext;
            Users = new UserRepository(_blogifyDbContext);
            BlogPosts = new BlogPostRepository(_blogifyDbContext);
            Comments = new CommentRepository(_blogifyDbContext);
            Likes = new LikeRepository(_blogifyDbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _blogifyDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _blogifyDbContext.Dispose();
        }
    }
}
