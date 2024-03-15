using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using WebFramework.Api;
using WebFramework.Filters;

namespace MyApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _repository;
        private readonly IMapper mapper;

        public PostsController(IRepository<Post> repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> Get(CancellationToken cancellationToken)
        {
            #region Old code
            //var posts = await _repository.TableNoTracking.Include(p => p.Category).Include(p => p.Author).ToListAsync(cancellationToken);

            //var list = posts.Select(p =>
            //{
            //    var dto = mapper.Map<PostDto>(p);
            //    return dto;
            //}).ToList();

            //var list = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    CategoryId = p.CategoryId,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name,
            //}).ToListAsync(cancellationToken);
            #endregion

            var list = await _repository.TableNoTracking.ProjectTo<PostDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ApiResult<PostDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<PostDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == id ,cancellationToken);

            //Post post = null; //Find from database by Id(include)
            //var resultDto = PostDto.FromEntity(mapper, post);

            if (dto == null)
                return NotFound();

            #region Old code
            //var dto = new PostDto
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    AuthorFullName = model.Author.FullName,
            //    CategoryName = model.Category.Name,
            //};
            #endregion

            return dto;
        }

        [HttpPost]
        public async Task<ApiResult<PostDto>> Create(PostDto dto, CancellationToken cancellationToken)
        {
            //var model = mapper.Map<Post>(dto);
            var model = dto.ToEntity(mapper);

            #region Old code
            //var model = new Post
            //{
            //    Title = dto.Title,
            //    Description = dto.Description,
            //    CategoryId = dto.CategoryId,
            //    AuthorId = dto.AuthorId
            //};
            #endregion

            await _repository.AddAsync(model, cancellationToken);

            #region Old code
            //await _repository.LoadReferenceAsync(model, p => p.Category, cancellationToken);
            //await _repository.LoadReferenceAsync(model, p => p.Author, cancellationToken);
            //model = await _repository.TableNoTracking
            //    .Include(p => p.Category)
            //    .Include(p => p.Author)
            //    .SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            //var resultDto = new PostDto
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    AuthorName = model.Author.FullName,
            //    CategoryName = model.Category.Name,
            //};

            //var resultDto = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    CategoryId = p.CategoryId,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name,
            //}).SingleOrDefaultAsync(cancellationToken);
            #endregion

            var resultDto = await _repository.TableNoTracking.ProjectTo<PostDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == model.Id ,cancellationToken);
            
            return resultDto;
        }

        [HttpGet("Test")]
        public ActionResult Test()
        {
            return Content("This is test");
        }

        [HttpPut]
        public async Task<ApiResult<PostDto>> Update(Guid id, PostDto dto, CancellationToken cancellationToken)
        {
            //var postDto = new PostDto();
            //Create
            //var post = postDto.ToEntity(mapper); // DTO => Entity
            //Update
            //var updatePost = postDto.ToEntity(mapper, model);  // DTO => Entity (an exist)
            //GetById
            //var postDto = PostDto.FromEntity(mapper, model); // Entity => DTO

            var model = await _repository.GetByIdAsync(cancellationToken, id);

            //mapper.Map(dto, model);
            dto.ToEntity(mapper, model);

            #region Old code
            //model.Title = dto.Title;
            //model.Description = dto.Description;
            //model.CategoryId = dto.CategoryId;
            //model.AuthorId = dto.AuthorId;
            #endregion

            await _repository.UpdateAsync(model, cancellationToken);

            #region Old code
            //var resultDto = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    CategoryId = p.CategoryId,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name,
            //}).SingleOrDefaultAsync(cancellationToken);
            #endregion

            var resultDto = await _repository.TableNoTracking.ProjectTo<PostDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            return resultDto;
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);

            return Ok();
        }
    }
}
