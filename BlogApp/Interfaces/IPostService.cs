
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostService 
    {
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
        Task<PostDTO> GetPostAsync(Guid id);
        Task<Post> AddPostAsync(CreatePostDTO createPostDTO);
        Task DeletePostAsync(Guid id);
        
    }
}
