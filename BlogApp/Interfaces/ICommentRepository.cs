using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);

    }
}
