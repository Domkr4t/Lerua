using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.Products.Queries.GetProductsList
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>()
                   .ForMember(dto => dto.Id, opt => opt.MapFrom(p => p.Id))
                   .ForMember(dto => dto.Name, opt => opt.MapFrom(p => p.Name))
                   .ForMember(dto => dto.Price, opt => opt.MapFrom(p => p.Price));
        }
    }
}
