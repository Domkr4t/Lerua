using MediatR;

namespace Lerua.Application.ShoppingCarts.Queries.GetShoppingCartById
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
