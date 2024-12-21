using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public DeleteCustomerCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new Exception($"Customer (Id={request.Id}) not found.");
            }

            var cart = await _context.ShoppingCarts
                .Include(sc => sc.Items)
                .FirstOrDefaultAsync(sc => sc.CustomerId == customer.Id);

            if (cart != null)
            {
                _context.ShoppingCartItems.RemoveRange(cart.Items);
                _context.ShoppingCarts.Remove(cart);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
