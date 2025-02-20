
using AutoMapper;
using BlogApp.DTO;
using BlogApp.Exceptions;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Services;
using Moq;

namespace BlogApp.Tests
{
    public class BlogAppPostServiceTests
    {
        private readonly Mock<IPostRepository> _postRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PostService _postService;
        public BlogAppPostServiceTests()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _mapperMock = new Mock<IMapper>();
            _postService = new PostService(_postRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddPostAsync_ShoulAddPost_WhenTitleDoesntExist()
        {
            var createPostDTO = new CreatePostDTO
            {
                Title = "New Title",
                Content = "Some Content for this post",
            };

            var post = new Post
            {
                Title = createPostDTO.Title,
                Content = createPostDTO.Content,
                CreatedAt = DateTime.UtcNow,
            };

            _postRepositoryMock.Setup(r => r.ExistingByTitleAsync(createPostDTO.Title))
                .ReturnsAsync(false);
            _mapperMock.Setup(m => m.Map<Post>(createPostDTO)).Returns(post);
            _postRepositoryMock.Setup(r => r.AddPostAsync(post)).Returns(Task.CompletedTask);

            var result = await _postService.AddPostAsync(createPostDTO);

            Assert.Equal(post, result);
            _postRepositoryMock.Verify(r=>r.AddPostAsync(post), Times.Once());
        }

        [Fact]

        public async Task AddPostAsync_ShoudThrowArgumentException_WhenTitleExist()
        {
            var createPostDTO = new CreatePostDTO
            {
                Title = "Duplicate Title",
                Content = "Some Content for this post"
            };

            _postRepositoryMock.Setup(r => r.ExistingByTitleAsync(createPostDTO.Title   )).ReturnsAsync(true);
        }

        [Fact]
        public async Task DeletePostAsync_ShouldDeletePost_WhenPostExist()
        {
            var postId = Guid.NewGuid();
            var post = new Post
            {
                Id = postId,
                Title = "Title",
                Content = "Some Content for this post",
            };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync(post);
            _postRepositoryMock.Setup(r => r.DeletePostAsync(post)).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task DeletePostAsync_ShouldThrowNotFoundPostByIdException_WhenPostDoesntExist()
        {
            var postId = Guid.NewGuid();
            _postRepositoryMock.Setup(r => r.GetPostAsync(postId)).ReturnsAsync((Post)null);

            await Assert.ThrowsAsync<NotFoundPostByIdException>(() => _postService.DeletePostAsync(postId));
        }

        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnMappedPosts()
        {
            var posts = new List<Post>()
            {
                new Post { Id = Guid.NewGuid(), Title = "Title1", Content = "Content1", CreatedAt = DateTime.UtcNow },
                new Post { Id = Guid.NewGuid(), Title = "Title2", Content = "Content2", CreatedAt = DateTime.UtcNow }
            };

            var postDTOs = new List<PostDTO>
            {
                new PostDTO { Id = posts[0].Id, Title = posts[0].Title, Content = posts[0].Content },
                new PostDTO { Id = posts[1].Id, Title = posts[1].Title, Content = posts[1].Content }
            };

            _postRepositoryMock.Setup(r => r.GetAllPostsAsync())
                               .ReturnsAsync(posts);
            _mapperMock.Setup(m => m.Map<IEnumerable<PostDTO>>(posts))
                       .Returns(postDTOs);

            
            var result = await _postService.GetAllPostsAsync();

            
            Assert.Equal(postDTOs, result);
        }

        [Fact]
        public async Task GetPostAsync_ShouldReturnMappedPost_WhenPostExists()
        {
            
            var postId = Guid.NewGuid();
            var post = new Post
            {
                Id = postId,
                Title = "Title",
                Content = "Content",
                CreatedAt = DateTime.UtcNow
            };
            var postDTO = new PostDTO
            {
                Id = postId,
                Title = "Title",
                Content = "Content"
            };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId))
                               .ReturnsAsync(post);
            _mapperMock.Setup(m => m.Map<PostDTO>(post))
                       .Returns(postDTO);

            
            var result = await _postService.GetPostAsync(postId);

            
            Assert.Equal(postDTO, result);
        }

        [Fact]
        public async Task GetPostAsync_ShouldThrowNotFoundPostByIdException_WhenPostDoesNotExist()
        {
           
            var postId = Guid.NewGuid();
            _postRepositoryMock.Setup(r => r.GetPostAsync(postId))
                               .ReturnsAsync((Post)null);

           
            await Assert.ThrowsAsync<NotFoundPostByIdException>(() => _postService.GetPostAsync(postId));
        }

        [Fact]
        public async Task UpdatePostAsync_ShouldUpdateAndReturnMappedPost_WhenPostExists()
        {
           
            var postId = Guid.NewGuid();
            var updatePostDTO = new UpdatePostDTO
            {
                Title = "Updated Title",
                Content = "Updated Content"
            };
            var post = new Post
            {
                Id = postId,
                Title = "Old Title",
                Content = "Old Content",
                CreatedAt = DateTime.UtcNow
            };
            var updatedPostDTO = new PostDTO
            {
                Id = postId,
                Title = "Updated Title",
                Content = "Updated Content"
            };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId))
                               .ReturnsAsync(post);
            _mapperMock.Setup(m => m.Map(updatePostDTO, post))
                       .Callback<UpdatePostDTO, Post>((dto, p) =>
                       {
                           p.Title = dto.Title;
                           p.Content = dto.Content;
                       });
            _postRepositoryMock.Setup(r => r.UpdatePostAsync(post))
                               .Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<PostDTO>(post))
                       .Returns(updatedPostDTO);

            
            var result = await _postService.UpdatePostAsync(postId, updatePostDTO);

          
            Assert.Equal(updatedPostDTO, result);
            _postRepositoryMock.Verify(r => r.UpdatePostAsync(post), Times.Once);
        }

        [Fact]
        public async Task UpdatePostAsync_ShouldThrowNotFoundPostByIdException_WhenPostDoesNotExist()
        {
          
            var postId = Guid.NewGuid();
            var updatePostDTO = new UpdatePostDTO
            {
                Title = "Updated Title",
                Content = "Updated Content"
            };

            _postRepositoryMock.Setup(r => r.GetPostAsync(postId))
                               .ReturnsAsync((Post)null);

          
            await Assert.ThrowsAsync<NotFoundPostByIdException>(() => _postService.UpdatePostAsync(postId, updatePostDTO));
        }
    }
}
