using BlogApp.DTO;

namespace BlogApp.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
        Task<CommentDTO> GetCommentAsync(Guid id);
        Task AddCommnetAsync(CommentDTO commnetDTO);
        Task DeletePostAsync(Guid id);
    }
}
