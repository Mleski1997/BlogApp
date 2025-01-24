using BlogApp.Dto;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostService 
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostAsync(int id);
        
    }
}
