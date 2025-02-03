using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post>GetPostAsync(Guid id);
        Task AddPostAsync(Post post);
        Task DeletePostAsync(Post post);
        Task <bool>ExistingByTitleAsync(string title);

    }
}
