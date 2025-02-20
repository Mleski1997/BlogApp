using BlogApp.Data;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Tests
{
    public class BlogAppCommentRepositoryTests : IDisposable
    {

        private readonly ApplicationDbContext _context;
        private readonly ICommentRepository _commentRepository;
        public BlogAppCommentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);
            _commentRepository = new CommentRepository(_context);

        }

        [Fact]

        public async Task AddCommentAsync_ShouldAddComment()
        {
            var comment = new Comment
            {
                Content = "Hello test content 123 123"
            };

            await _commentRepository.AddCommentAsync(comment);
            var result = await _context.Comments.ToListAsync();


            Assert.Single(result);
            Assert.Equal("Hello test content 123 123", result.First().Content);
        }

        [Fact]
        public async Task DeleteCommentASync_ShouldDeleteComment()
        {
            var comment = new Comment
            {
                Content = "Hello test content 123 123"
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            await _commentRepository.DeleteCommentAsync(comment);
            var result = await _context.Comments.ToListAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllCommentsAsync_ShouldReturnAllComments()
        {
            var comment1 = new Comment
            {
                Content = "Hello test content 123 123"
            };
            var comment2 = new Comment
            {
                Content = "Hello test content 999 999"
            };

            await _context.Comments.AddRangeAsync(comment1, comment2);
            await _context.SaveChangesAsync();

            var result = await _commentRepository.GetAllCommentsAsync();

            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetCommentByIdAsync_ShouldReturnCommentById()
        {
            var comment = new Comment
            {
                Content = "Hello test content 123 123"
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            var result = await _commentRepository.GetCommentByIdAsync(comment.Id);

            Assert.NotNull(result);
            Assert.Equal("Hello test content 123 123", result.Content);
        }

        [Fact]

        public async Task GetPostCommentsAsync_ShouldReturnsTopLevelComment_WithReplies()
        {
            var postId = Guid.NewGuid();

            var topLevelComment1 = new Comment
            {
                PostId = postId,
                ParentCommentId = null,
                Content = "Main comment 1"
            };

            var topLevelComment2 = new Comment
            {
                PostId = postId,
                ParentCommentId = null,
                Content = "Main comment 2"
            };

            var replyComment1 = new Comment
            {
                PostId = postId,
                ParentCommentId = topLevelComment2.Id,
                Content = "Reply to main comment 2"
            };

            var commentToOtherPost = new Comment
            {
                PostId = Guid.NewGuid(),
                ParentCommentId = null,
                Content = "Content for other post"
            };

            _context.Comments.AddRangeAsync(topLevelComment1, topLevelComment2,  replyComment1, commentToOtherPost);
            await _context.SaveChangesAsync();

            var result = await _commentRepository.GetPostCommentsAsync(postId);

            Assert.Equal(2, result.Count());

            var secondComment = result.FirstOrDefault(c => c.Id == topLevelComment2.Id);
            Assert.NotNull(secondComment);
            Assert.NotNull(secondComment.Replies);
            Assert.Single(secondComment.Replies);
            Assert.Equal("Reply to main comment 2", secondComment.Replies.First().Content);
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
