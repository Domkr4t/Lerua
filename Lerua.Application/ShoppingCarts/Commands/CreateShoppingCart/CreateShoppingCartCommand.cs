using MediatR;

namespace Lerua.Application.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
    }
}
