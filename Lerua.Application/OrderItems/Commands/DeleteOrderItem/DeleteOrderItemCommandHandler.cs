using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public DeleteOrderItemCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == request.Id, cancellationToken);

            if (orderItem == null)
            {
                throw new Exception($"OrderItem (Id={request.Id}) not found.");
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
