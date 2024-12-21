using MediatR;

namespace Lerua.Application.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
