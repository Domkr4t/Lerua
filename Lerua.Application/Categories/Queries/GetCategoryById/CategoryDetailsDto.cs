using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Categories.Queries.GetCategoryById
{
    public class CategoryDetailsDto : IMapWith<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDetailsDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
