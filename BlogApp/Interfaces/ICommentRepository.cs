using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>>GetPostCommentsAsync(Guid postId);
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);

    }
}
