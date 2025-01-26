using AutoMapper;
using BlogApp.DTO;
using BlogApp.Models;

namespace BlogApp.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostDTO, Post>();
        }
    }
}
