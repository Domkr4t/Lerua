using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.ShoppingCartItems.Commands.CreateShoppingCartItem
{
    public class CreateShoppingCartItemCommandHandler : IRequestHandler<CreateShoppingCartItemCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public CreateShoppingCartItemCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            // Здесь можно проверить, что такая корзина существует, что продукт существует и т.д.
            var item = new ShoppingCartItem
            {
                // Если у вас композитный ключ, Id нет.
                // Важно задать ShoppingCartId, если вы его добавили в модель. 
                // (В коде домена нужно скорректировать модель, чтобы хранить ShoppingCartId.)
                ShoppingCartId = request.ShoppingCartId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            // Т.к. ShoppingCartItem иногда связана напрямую:
            // - Либо через DbSet<ShoppingCartItem> 
            // - Либо добавлять в Items коллекцию корзины (а потом SaveChanges)
            // Для простоты положим прямо в DbSet:
            _context.ShoppingCartItems.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
