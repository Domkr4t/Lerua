using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Products.Queries.GetProductById
{
    public class ProductDetailsDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
