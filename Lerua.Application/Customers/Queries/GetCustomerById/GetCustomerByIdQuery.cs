using MediatR;

namespace Lerua.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
