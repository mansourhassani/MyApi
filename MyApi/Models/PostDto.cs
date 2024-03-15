﻿using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Entities;
using WebFramework.Api;

namespace MyApi.Models
{
    public class PostDto : BaseDto<PostDto, Post, Guid>
    { 
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }

    public class PostSelectDto : BaseDto<PostSelectDto, Post, Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; } //Category.Name
        public string? AuthorFullName { get; set; } //Author.Name
        public string? FullTitle { get; set; } // => mapped from "Title (Category.Name)"

        public override void CustomMappings(IMappingExpression<Post, PostSelectDto> mapping)
        {
            mapping.ForMember(
                dest => dest.FullTitle,
                config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
        }
    }
}
