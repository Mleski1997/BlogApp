using AutoMapper;
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<CreatePostDTO, Post>();
            CreateMap<Post, CreatePostDTO>();
            CreateMap<Comment, CreateCommentDTO>();
            CreateMap<CreateCommentDTO, Comment>();
        }
    }
}
