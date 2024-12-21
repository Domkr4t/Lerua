using MediatR;

namespace Lerua.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
