using Blogify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Infrastructure.Data
{
    public class BlogifyDbContext : DbContext
    {
        public BlogifyDbContext(DbContextOptions<BlogifyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<BlogPost>(bp =>
            {
                bp.HasKey(bp => bp.Id);
                bp.Property(bp => bp.Title).IsRequired();
                bp.Property(bp => bp.Content).IsRequired();
                bp.Property(bp => bp.CreatedDate).HasDefaultValueSql("GETDATE()");
                bp.HasOne(bp => bp.User)
                    .WithMany(u => u.BlogPosts)
                    .HasForeignKey(bp => bp.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comment>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.Content).IsRequired();
                c.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");
                c.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                c.HasOne(c => c.BlogPost)
                    .WithMany(bp => bp.Comments)
                    .HasForeignKey(c => c.BlogPostId)
                    .OnDelete(DeleteBehavior.Restrict);
                c.HasOne(c => c.ParentComment)
                    .WithMany(c => c.Replies)
                    .HasForeignKey(c => c.ParentCommentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Like>(l =>
            {
                l.HasKey(l => l.Id);
                l.HasOne(l => l.User)
                    .WithMany(u => u.Likes)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                l.HasOne(l => l.BlogPost)
                    .WithMany(bp => bp.Likes)
                    .HasForeignKey(l => l.BlogPostId)
                    .OnDelete(DeleteBehavior.Restrict);
                l.HasOne(l => l.Comment)
                    .WithMany(c => c.Likes)
                    .HasForeignKey(l => l.CommentId)
                    .OnDelete(DeleteBehavior.Restrict);

                l.HasIndex(l => new { l.UserId, l.BlogPostId, }).IsUnique();
                l.HasIndex(l => new { l.UserId, l.CommentId, }).IsUnique();
            });
        }
    }
}
