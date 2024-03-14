using AutoMapper;
using Entities;
using MyApi.Models;

namespace MyApi
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap()
                .ForMember(p => p.Author, opt => opt.Ignore())
                .ForMember(p => p.Category, opt => opt.Ignore());
        }
    }
}
