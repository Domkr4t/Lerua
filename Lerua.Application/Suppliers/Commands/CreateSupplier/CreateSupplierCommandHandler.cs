using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateSupplierCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ContactInfo = request.ContactInfo
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(cancellationToken);

            return supplier.Id;
        }
    }
}
