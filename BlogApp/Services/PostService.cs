
using AutoMapper;
using BlogApp.DTO;
using BlogApp.Exceptions;
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

        public async Task<Post> AddPostAsync(CreatePostDTO createPostDTO)
        {

            if  (await _postRepository.ExistingByTitleAsync(createPostDTO.Title))
                throw new ArgumentException("A post with this title already exists.");

            var post = _mapper.Map<Post>(createPostDTO); 
            await _postRepository.AddPostAsync(post);

            return post;
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await _postRepository.GetPostAsync(id);

            if (post == null)
            {
                throw new NotFoundPostByIdException();
            }
   
            await _postRepository.DeletePostAsync(post);
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }

        public async Task<PostDTO> GetPostAsync(Guid id)
        {
            var post = await _postRepository.GetPostAsync(id);
            if (post == null)
            {
                throw new NotFoundPostByIdException();
            }
            return _mapper.Map<PostDTO>(post);
        }
    }
}
