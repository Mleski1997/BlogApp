﻿using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
        Task<CommentDTO> GetCommentAsync(Guid id);
        Task<List<CommentDTO>> GetPostCommentsAsync(Guid postId);
        Task <Comment>AddCommentAsync(CreateCommentDTO createCommentDTO);
        Task DeleteCommentAsync(Guid id);
    }
}
