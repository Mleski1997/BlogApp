
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostService 
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostAsync(int id);
        Task AddPostAsync(PostDTO postDTO);
        
    }
}
