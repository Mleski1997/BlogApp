
using BlogApp.DTO;
using BlogApp.Interfaces;
using BlogApp.Models;

namespace BlogApp.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task AddPostAsync(PostDTO postDTO)
        {
            var Post = new Post
            {
                Title = postDTO.Title,
                Content = postDTO.Content,
                CreatedAt = DateTime.Now,
            };
            await _postRepository.AddPostAsync(Post);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public Task<Post> GetPostAsync(int id)
        {
            return _postRepository.GetPostAsync(id);
        }
    }
}
