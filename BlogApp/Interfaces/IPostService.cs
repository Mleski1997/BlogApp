
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostService 
    {
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
        Task<PostDTO> GetPostAsync(int id);
        Task AddPostAsync(PostDTO postDTO);
        Task DeletePostAsync(int id);
        
    }
}
