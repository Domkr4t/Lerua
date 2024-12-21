using MediatR;

namespace Lerua.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
