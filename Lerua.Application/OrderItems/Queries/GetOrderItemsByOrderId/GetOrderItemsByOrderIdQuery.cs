using MediatR;

namespace Lerua.Application.OrderItems.Queries.GetOrderItemsByOrderId
{
    public class GetOrderItemsByOrderIdQuery : IRequest<List<OrderItemLookupDto>>
    {
        public Guid OrderId { get; set; }
    }
}
