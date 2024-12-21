using MediatR;

namespace Lerua.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string ContactInfo { get; set; } = default!;
    }
}
