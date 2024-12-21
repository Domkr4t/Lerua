using MediatR;

namespace Lerua.Application.ShoppingCartItems.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommand : IRequest<Unit>
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
