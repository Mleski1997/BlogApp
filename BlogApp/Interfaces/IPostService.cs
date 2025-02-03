
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostService 
    {
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
        Task<PostDTO> GetPostAsync(Guid id);
        Task AddPostAsync(PostDTO postDTO);
        Task DeletePostAsync(Guid id);
        
    }
}
