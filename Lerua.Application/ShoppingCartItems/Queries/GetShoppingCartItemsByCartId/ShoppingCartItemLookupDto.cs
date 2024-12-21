using AutoMapper;
using Lerua.Application.Common.Mappings;
using Lerua.Domain;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemsByCartId
{
    public class ShoppingCartItemLookupDto : IMapWith<ShoppingCartItem>
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCartItem, ShoppingCartItemLookupDto>();
        }
    }
}
