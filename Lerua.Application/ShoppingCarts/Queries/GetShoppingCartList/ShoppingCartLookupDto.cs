using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.ShoppingCarts.Queries.GetShoppingCartList
{
    public class ShoppingCartLookupDto : IMapWith<ShoppingCart>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public List<ShoppingCartItemDto> Items { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dto => dto.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dto => dto.Items,
                           opt => opt.MapFrom(src => src.Items));
        }
    }
}
