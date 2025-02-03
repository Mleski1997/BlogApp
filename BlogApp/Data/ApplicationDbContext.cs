using BlogApp.Exceptions;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //var postId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            //var commandId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired(false);


            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    Id = postId,
            //    Title = "Test Title1",
            //    Content = "hello its my test description",
            //    CreatedAt = new DateTime(2025, 01, 01),
            //});

            //modelBuilder.Entity<Comment>().HasData(new Comment
            //{
            //    Id = commandId,
            //    Content = "test test test",
            //    AuthorName = "Author 1",
            //    CreatedAt = new DateTime(2025, 01, 01),
            //    PostId = postId,
            //});


        }

    }
}
