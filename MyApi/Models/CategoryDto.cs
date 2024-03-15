using AutoMapper;
using Entities;
using WebFramework.Api;

namespace MyApi.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category>
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? ParentCategoryName { get; set; } //=> mapped from ParentCategory.Name
    }
}
