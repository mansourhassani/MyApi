﻿//using AutoMapper;
//using Entities;
//using WebFramework.CustomMapping;

//namespace MyApi.Models
//{
//    public class PostCustomMapping : IHaveCustomMapping
//    {
//        public void CreateMappings(Profile profile)
//        {
//            profile.CreateMap<Post, PostDto>().ReverseMap()
//                .ForMember(p => p.Author, opt => opt.Ignore())
//                .ForMember(p => p.Category, opt => opt.Ignore());
//        }
//    }

//    public class CategoryCustomMapping : IHaveCustomMapping
//    {
//        public void CreateMappings(Profile profile)
//        {
//            profile.CreateMap<Category, CategoryDto>().ReverseMap();
//        }
//    }
//}
