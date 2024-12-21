using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemList
{
    public class ShoppingCartItemLookupDto : IMapWith<ShoppingCartItem>
    {
        // Допустим, у нас ShoppingCartId и ProductId - композитный ключ
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCartItem, ShoppingCartItemLookupDto>()
                .ForMember(dto => dto.ShoppingCartId, opt => opt.MapFrom(src => src.ShoppingCartId))
                .ForMember(dto => dto.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dto => dto.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
