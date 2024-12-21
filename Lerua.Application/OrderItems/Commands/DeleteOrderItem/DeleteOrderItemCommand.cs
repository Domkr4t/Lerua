using MediatR;

namespace Lerua.Application.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
