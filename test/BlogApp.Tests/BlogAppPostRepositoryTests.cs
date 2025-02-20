using BlogApp.Data;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Tests
{
    public class BlogAppPostRepositoryTest : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostRepository _postRepository;
        public BlogAppPostRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);
            _postRepository = new PostRepository(_context);

        }


        [Fact]
        public async Task AddPostAsync_ShouldAddPost()
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post",
                Content = "Hello its conntnetn for this post"
            };

            
            await _postRepository.AddPostAsync(post);
            var posts = await _context.Posts.ToListAsync();

            
            Assert.Single(posts);
            Assert.Equal("Test Post", posts.First().Title);
            Assert.Equal("Hello its conntnetn for this post", posts.First().Content);
        }

        [Fact]
        public async Task DeletePostAsync_ShouldDeletePost()
        {

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post",
                Content = "Hello its conntnetn for this post"
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            
            await _postRepository.DeletePostAsync(post);
            var posts = await _context.Posts.ToListAsync();

            
            Assert.Empty(posts);
        }

        [Fact]
        public async Task ExistingByTitleAsync_ShouldReturnTrue_IfPostExist()
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post",
                Content = "Hello its conntnetn for this post"
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            var exist = await _postRepository.ExistingByTitleAsync("Test Post");

            Assert.True(exist);
        }

        [Fact]
        public async Task ExistingByTitle_ShouldReturnFalse_IfPostDoesNotExist()
        {
            var exist = await _postRepository.ExistingByTitleAsync("Not Exist Post");

            Assert.False(exist);
        }

        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnAllPosts()
        {
            var post1 = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Content = "Hello its conntnetn for this post 1"
            };
            var post2 = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 2",
                Content = "Hello its conntnetn for this post2"
            };
            await _context.Posts.AddRangeAsync(post1, post2);
            await _context.SaveChangesAsync();

            var posts = await _postRepository.GetAllPostsAsync();

            Assert.Equal(2, posts.Count());
        }

        [Fact]
        public async Task GetPostAsync_ShouldReurnPost()
        {
            var post1 = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Content = "Hello its conntnetn for this post 1"
            };

            await _context.Posts.AddAsync(post1);
            await _context.SaveChangesAsync();

            var post = await _postRepository.GetPostAsync(post1.Id);

            Assert.NotNull(post);
            Assert.Equal("Test Post 1", post.Title);
        }

        [Fact]
        public async Task UpdatePostAsync_ShouldUpdatePost()
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Content = "Hello its conntnetn for this post 1"
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            post.Title = "New Title Post";
            post.Content = "New Content for this Post";

            await _postRepository.UpdatePostAsync(post);
            var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            
            Assert.NotNull(result);
            Assert.Equal("New Title Post", result.Title);
            Assert.Equal("New Content for this Post", result.Content);
        }

        public void Dispose()
        { 
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}