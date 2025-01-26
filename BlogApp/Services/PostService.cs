
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

            if  (await _postRepository.ExistingByTitleAsync(postDTO.Title))
                throw new ArgumentException("A post with this title already exist.");
               
                var Post = new Post
                {
                    Title = postDTO.Title,
                    Content = postDTO.Content,
                    CreatedAt = DateTime.Now,
                };
                await _postRepository.AddPostAsync(Post);   
        }

        public async Task DeletePostAsync(int id)
        {
            var postExist = await _postRepository.GetPostAsync(id);
            if (postExist == null)
            {
                throw new ArgumentException("Post doesnt exist");
            }
            var post = await _postRepository.GetPostAsync(id);
            await _postRepository.DeletePostAsync(post);
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
