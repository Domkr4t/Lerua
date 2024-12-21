using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.ShoppingCartItems.Commands.DeleteShoppingCartItem
{
    public class DeleteShoppingCartItemCommandHandler
        : IRequestHandler<DeleteShoppingCartItemCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public DeleteShoppingCartItemCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i =>
                    i.ShoppingCartId == request.ShoppingCartId
                    && i.ProductId == request.ProductId, cancellationToken);

            if (item == null)
            {
                throw new Exception($"ShoppingCartItem not found for CartId={request.ShoppingCartId}, ProductId={request.ProductId}.");
            }

            _context.ShoppingCartItems.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
