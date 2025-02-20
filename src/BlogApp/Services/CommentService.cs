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
        private readonly IPostRepository _postRepository;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Comment> AddCommentAsync(Guid postId, CreateCommentDTO createCommentDTO)
        {
            var post = await _postRepository.GetPostAsync(postId);
            if (post == null)
            {
                throw new NotFoundPostByIdException(postId);
            }

            var comment = _mapper.Map<Comment>(createCommentDTO);
            comment.PostId = postId;
            await _commentRepository.AddCommentAsync(comment);
            return comment;
        }

        public async Task<Comment> AddReplyAsync(Guid postId, Guid parentCommentId, CreateCommentDTO createCommentDTO)
        {
            var post = await _postRepository.GetPostAsync(postId);
            if (post == null)
            {
                throw new NotFoundPostByIdException(postId);
            }
            var parentComment = await _commentRepository.GetCommentByIdAsync(parentCommentId);
            
            if (parentComment == null)
            {
                throw new NotFoundCommentByIdException(parentCommentId);
            }

            var comment = _mapper.Map<Comment>(createCommentDTO);
            comment.PostId = postId;
            comment.ParentCommentId = parentCommentId;
            await _commentRepository.AddCommentAsync(comment);
            return comment;
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                throw new NotFoundCommentByIdException();
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
                throw new NotFoundCommentByIdException();
            }
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<List<CommentDTO>> GetPostCommentsAsync(Guid postId)
        {
            var comments = await _commentRepository.GetPostCommentsAsync(postId);
            return _mapper.Map<List<CommentDTO>>(comments);
        }
    }
}
