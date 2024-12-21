using MediatR;

namespace Lerua.Application.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
