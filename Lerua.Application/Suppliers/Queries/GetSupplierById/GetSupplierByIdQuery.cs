using MediatR;

namespace Lerua.Application.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQuery : IRequest<SupplierDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
