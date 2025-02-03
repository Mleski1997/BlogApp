using AutoMapper;
using BlogApp.DTO;
using BlogApp.Exceptions;
using BlogApp.Interfaces;
using BlogApp.Models;
using BlogApp.Repository;

namespace BlogApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task AddCommentAsync(CommentDTO commnetDTO)
        {
            var comment = _mapper.Map<Comment>(commnetDTO);
            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                throw new NotFoundPostByIdException();
            }

            await _commentRepository.DeleteCommentAsync(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);

        }

        public async Task<CommentDTO> GetCommentAsync(Guid id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                throw new NotFoundPostByIdException();
            }
            return _mapper.Map<CommentDTO>(comment);
        }
    }
}
