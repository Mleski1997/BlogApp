using BlogApp.DTO;

namespace BlogApp.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
        Task<CommentDTO> GetCommentAsync(Guid id);
        Task AddCommentAsync(CommentDTO commnetDTO);
        Task DeleteCommentAsync(Guid id);
    }
}
