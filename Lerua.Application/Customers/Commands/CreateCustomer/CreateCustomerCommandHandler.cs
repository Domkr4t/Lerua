using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateCustomerCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var shoppingCard = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
            };

            _context.Customers.Add(customer);
            _context.ShoppingCarts.Add(shoppingCard);
            await _context.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}
