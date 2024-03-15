using AutoMapper;
using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using WebFramework.Api;

namespace MyApi.Controllers
{
    public class PostsController : CrudController<PostDto, PostSelectDto, Post, Guid>
    {
        public PostsController(IRepository<Post> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
