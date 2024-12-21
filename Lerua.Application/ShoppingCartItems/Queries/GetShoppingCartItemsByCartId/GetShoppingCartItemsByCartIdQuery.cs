using MediatR;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemsByCartId
{
    public class GetShoppingCartItemsByCartIdQuery : IRequest<List<ShoppingCartItemLookupDto>>
    {
        public Guid CartId { get; set; }
    }
}
