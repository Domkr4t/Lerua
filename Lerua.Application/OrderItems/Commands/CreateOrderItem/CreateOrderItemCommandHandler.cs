using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateOrderItemCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var item = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Price = request.Price
            };

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
