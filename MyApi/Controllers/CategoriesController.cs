using AutoMapper;
using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using WebFramework.Api;

namespace MyApi.Controllers
{
    public class CategoriesController : CrudController<CategoryDto, Category>
    {
        public CategoriesController(IRepository<Category> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
