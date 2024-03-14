using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Entities;

namespace MyApi.Models
{
    public class PostDto // => Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        
        public string? CategoryName { get; set; } //Category.Name
        public string? AuthorFullName { get; set; } //Author.Name
    }
}
