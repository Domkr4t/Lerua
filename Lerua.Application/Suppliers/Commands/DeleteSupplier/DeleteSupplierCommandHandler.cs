using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public DeleteSupplierCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (supplier == null)
            {
                throw new Exception($"Supplier (Id={request.Id}) not found.");
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
