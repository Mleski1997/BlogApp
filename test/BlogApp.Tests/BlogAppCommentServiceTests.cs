using AutoMapper;
using BlogApp.DTO;
using BlogApp.Exceptions;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Tests
{
    public class BlogAppCommentServiceTests
    {
        private readonly Mock<ICommentRepository> _commentRepositoryMock;
        private readonly Mock<IPostRepository> _postRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CommentService _commentService;

        public BlogAppCommentServiceTests()
        {
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _postRepositoryMock = new Mock<IPostRepository>();
            _mapperMock = new Mock<IMapper>();
            _commentService = new CommentService(_commentRepositoryMock.Object, _mapperMock.Object, _postRepositoryMock.Object);
        }

        [Fact]
        public async Task AddCommentAsync_ShouldAddComment_WhenPostExists()
        {
           
            var postId = Guid.NewGuid();
            var createCommentDTO = new CreateCommentDTO { Content = "Test comment" };
            var post = new Post { Id = postId, Title = "Test post", Content = "Test content", CreatedAt = DateTime.UtcNow };
            var mappedComment = new Comment { Id = Guid.NewGuid(), Content = createCommentDTO.Content, PostId = postId };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync(post);
            _mapperMock.Setup(m => m.Map<Comment>(createCommentDTO)).Returns(mappedComment);
            _commentRepositoryMock.Setup(r => r.AddCommentAsync(mappedComment)).Returns(Task.CompletedTask);

          
            var result = await _commentService.AddCommentAsync(postId, createCommentDTO);

          
            Assert.Equal(postId, result.PostId);
            Assert.Equal(createCommentDTO.Content, result.Content);
            _commentRepositoryMock.Verify(r => r.AddCommentAsync(mappedComment), Times.Once);
        }

        [Fact]
        public async Task AddCommentAsync_ShouldThrowNotFoundPostByIdException_WhenPostDoesNotExist()
        {
           
            var postId = Guid.NewGuid();
            var createCommentDTO = new CreateCommentDTO { Content = "Test comment" };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync((Post)null);

            await Assert.ThrowsAsync<NotFoundPostByIdException>(() => _commentService.AddCommentAsync(postId, createCommentDTO));
        }

       
        [Fact]
        public async Task AddReplyAsync_ShouldAddReply_WhenPostAndParentCommentExist()
        {
            
            var postId = Guid.NewGuid();
            var parentCommentId = Guid.NewGuid();
            var createCommentDTO = new CreateCommentDTO { Content = "Reply comment" };
            var post = new Post { Id = postId, Title = "Test post", Content = "Test content", CreatedAt = DateTime.UtcNow };
            var parentComment = new Comment { Id = parentCommentId, Content = "Parent comment", PostId = postId };
            var mappedReply = new Comment
            {
                Id = Guid.NewGuid(),
                Content = createCommentDTO.Content,
                PostId = postId,
                ParentCommentId = parentCommentId
            };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync(post);
            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(parentCommentId)).ReturnsAsync(parentComment);
            _mapperMock.Setup(m => m.Map<Comment>(createCommentDTO)).Returns(mappedReply);
            _commentRepositoryMock.Setup(r => r.AddCommentAsync(mappedReply)).Returns(Task.CompletedTask);

            
            var result = await _commentService.AddReplyAsync(postId, parentCommentId, createCommentDTO);

            
            Assert.Equal(postId, result.PostId);
            Assert.Equal(parentCommentId, result.ParentCommentId);
            Assert.Equal(createCommentDTO.Content, result.Content);
            _commentRepositoryMock.Verify(r => r.AddCommentAsync(mappedReply), Times.Once);
        }

        [Fact]
        public async Task AddReplyAsync_ShouldThrowNotFoundPostByIdException_WhenPostDoesNotExist()
        {
            
            var postId = Guid.NewGuid();
            var parentCommentId = Guid.NewGuid();
            var createCommentDTO = new CreateCommentDTO { Content = "Reply comment" };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync((Post)null);

            
            await Assert.ThrowsAsync<NotFoundPostByIdException>(() => _commentService.AddReplyAsync(postId, parentCommentId, createCommentDTO));
        }

        [Fact]
        public async Task AddReplyAsync_ShouldThrowNotFoundCommentByIdException_WhenParentCommentDoesNotExist()
        {
            
            var postId = Guid.NewGuid();
            var parentCommentId = Guid.NewGuid();
            var createCommentDTO = new CreateCommentDTO { Content = "Reply comment" };
            var post = new Post { Id = postId, Title = "Test post", Content = "Test content", CreatedAt = DateTime.UtcNow };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync(post);
            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(parentCommentId)).ReturnsAsync((Comment)null);

            
            await Assert.ThrowsAsync<NotFoundCommentByIdException>(() => _commentService.AddReplyAsync(postId, parentCommentId, createCommentDTO));
        }

        
        [Fact]
        public async Task DeleteCommentAsync_ShouldDeleteComment_WhenCommentExists()
        {
            
            var commentId = Guid.NewGuid();
            var comment = new Comment { Id = commentId, Content = "Test comment" };

            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(commentId)).ReturnsAsync(comment);
            _commentRepositoryMock.Setup(r => r.DeleteCommentAsync(comment)).Returns(Task.CompletedTask);

            
            await _commentService.DeleteCommentAsync(commentId);

           
            _commentRepositoryMock.Verify(r => r.DeleteCommentAsync(comment), Times.Once);
        }

        [Fact]
        public async Task DeleteCommentAsync_ShouldThrowNotFoundCommentByIdException_WhenCommentDoesNotExist()
        {
            
            var commentId = Guid.NewGuid();
            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(commentId)).ReturnsAsync((Comment)null);

            
            await Assert.ThrowsAsync<NotFoundCommentByIdException>(() => _commentService.DeleteCommentAsync(commentId));
        }

       
        [Fact]
        public async Task GetAllCommentsAsync_ShouldReturnMappedCommentDTOs()
        {
            
            var comments = new List<Comment>
            {
                new Comment { Id = Guid.NewGuid(), Content = "Comment 1" },
                new Comment { Id = Guid.NewGuid(), Content = "Comment 2" }
            };

            var commentDTOs = new List<CommentDTO>
            {
                new CommentDTO { Id = comments[0].Id, Content = "Comment 1" },
                new CommentDTO { Id = comments[1].Id, Content = "Comment 2" }
            };

            _commentRepositoryMock.Setup(r => r.GetAllCommentsAsync()).ReturnsAsync(comments);
            _mapperMock.Setup(m => m.Map<IEnumerable<CommentDTO>>(comments)).Returns(commentDTOs);

            
            var result = await _commentService.GetAllCommentsAsync();

            
            Assert.Equal(commentDTOs, result);
        }

       
        [Fact]
        public async Task GetCommentAsync_ShouldReturnMappedCommentDTO_WhenCommentExists()
        {
            
            var commentId = Guid.NewGuid();
            var comment = new Comment { Id = commentId, Content = "Test comment" };
            var commentDTO = new CommentDTO { Id = commentId, Content = "Test comment" };

            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(commentId)).ReturnsAsync(comment);
            _mapperMock.Setup(m => m.Map<CommentDTO>(comment)).Returns(commentDTO);

            
            var result = await _commentService.GetCommentAsync(commentId);

            Assert.Equal(commentDTO, result);
        }

        [Fact]
        public async Task GetCommentAsync_ShouldThrowNotFoundCommentByIdException_WhenCommentDoesNotExist()
        {
            
            var commentId = Guid.NewGuid();
            _commentRepositoryMock.Setup(r => r.GetCommentByIdAsync(commentId)).ReturnsAsync((Comment)null);

            
            await Assert.ThrowsAsync<NotFoundCommentByIdException>(() => _commentService.GetCommentAsync(commentId));
        }

        
        [Fact]
        public async Task GetPostCommentsAsync_ShouldReturnMappedCommentDTOList()
        {
            
            var postId = Guid.NewGuid();
            var comments = new List<Comment>
            {
                new Comment { Id = Guid.NewGuid(), Content = "Comment 1", PostId = postId },
                new Comment { Id = Guid.NewGuid(), Content = "Comment 2", PostId = postId }
            };

            var commentDTOs = new List<CommentDTO>
            {
                new CommentDTO { Id = comments[0].Id, Content = "Comment 1" },
                new CommentDTO { Id = comments[1].Id, Content = "Comment 2" }
            };

            _commentRepositoryMock.Setup(r => r.GetPostCommentsAsync(postId)).ReturnsAsync(comments);
            _mapperMock.Setup(m => m.Map<List<CommentDTO>>(comments)).Returns(commentDTOs);

            
            var result = await _commentService.GetPostCommentsAsync(postId);

            
            Assert.Equal(commentDTOs, result);
        }
    }
}
    
