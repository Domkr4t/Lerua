using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateShoppingCartCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId
            };

            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync(cancellationToken);

            return cart.Id;
        }
    }
}
