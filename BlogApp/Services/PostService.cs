
using AutoMapper;
using BlogApp.DTO;
using BlogApp.Interfaces;
using BlogApp.Models;

namespace BlogApp.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task AddPostAsync(PostDTO postDTO)
        {

            if  (await _postRepository.ExistingByTitleAsync(postDTO.Title))
                throw new ArgumentException("A post with this title already exist.");

            var post = _mapper.Map<Post>(postDTO); 
            await _postRepository.AddPostAsync(post);   
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _postRepository.GetPostAsync(id)
                ?? throw new ArgumentException("Post doesn't exits");
            await _postRepository.DeletePostAsync(post);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return _mapper.Map<IEnumerable<Post>>(posts);
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await _postRepository.GetPostAsync(id);
            return _mapper.Map<Post>(post);
        }
    }
}
