using Lerua.Application.Common.Mappings;
using Lerua.Domain;
using AutoMapper;

namespace Lerua.Application.ShoppingCarts.Queries
{
    public class ShoppingCartItemDto : IMapWith<ShoppingCartItem>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
        }
    }
}
