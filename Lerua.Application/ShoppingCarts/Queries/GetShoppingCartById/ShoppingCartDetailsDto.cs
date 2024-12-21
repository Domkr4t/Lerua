using Lerua.Application.Common.Mappings;
using Lerua.Domain;
using AutoMapper;

namespace Lerua.Application.ShoppingCarts.Queries.GetShoppingCartById
{
    public class ShoppingCartDetailsDto : IMapWith<ShoppingCart>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public List<ShoppingCartItemDto> Items { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartDetailsDto>()
                .ForMember(dto => dto.Items,
                           opt => opt.MapFrom(src => src.Items));
        }
    }
}
