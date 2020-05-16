using AutoMapper;
using PersonalManager.Application.Common.Mappings;
using PersonalManager.Domain.Entities;

namespace PersonalManager.Application.Categories.Queries.GetCategories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public Category Parent { get; set; }

        public string IconUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>();
        }
    }
}
