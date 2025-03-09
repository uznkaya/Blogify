using Blogify.Application.Interfaces;
using Blogify.Domain.Entities;
using Blogify.Domain.Exceptions;
using Blogify.Infrastructure.Interfaces;

namespace Blogify.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogPostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBlogPostAsync(BlogPost blogPost)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == blogPost.UserId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            await _unitOfWork.BlogPosts.AddAsync(blogPost);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<BlogPost>> GetAllBlogPostAsync()
        {
            var blogposts = await _unitOfWork.BlogPosts.GetAllAsync();
            if (!blogposts.Any())
                throw new BlogPostNotFoundException();

            return blogposts;
        }
        public async Task UpdateBlogPostAsync(int blogPostId, BlogPost updatedBlogPost)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Id == updatedBlogPost.UserId && !u.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            blogPost.Title = updatedBlogPost.Title;
            blogPost.Content = updatedBlogPost.Content;
            blogPost.UserId = updatedBlogPost.UserId;

            await _unitOfWork.BlogPosts.UpdateAsync(blogPost);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteBlogPostAsync(int blogPostId)
        {
            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            await _unitOfWork.BlogPosts.DeleteBlogPostAsync(blogPostId);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<BlogPost> GetBlogPostByIdAsync(int blogPostId)
        {
            var blogPost = await _unitOfWork.BlogPosts.FindAsync(bp => bp.Id == blogPostId && !bp.IsDeleted);
            if (blogPost == null)
                throw new BlogPostNotFoundException();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPostsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.FindAsync(bp => bp.Id == userId && !bp.IsDeleted);
            if (user == null)
                throw new UserNotFoundException();

            var blogPosts = await _unitOfWork.BlogPosts.GetBlogPostsByUserIdAsync(user.Id);
            if (!blogPosts.Any())
                throw new BlogPostNotFoundException();

            return blogPosts;
        }

        public async Task<IEnumerable<BlogPost>> GetRecentBlogPostsAsync(int count)
        {
            var blogPosts = await _unitOfWork.BlogPosts.GetRecentBlogPostsAsync(count);
            if (!blogPosts.Any())
                throw new BlogPostNotFoundException();

            return blogPosts;
        }
    }
}
