using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateOrderCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.ShoppingCarts
                .Include(sc => sc.Items)
                .FirstOrDefaultAsync(sc => sc.CustomerId == request.CustomerId, cancellationToken);

            if (cart == null)
            {
                throw new Exception($"ShoppingCart not found for CustomerId={request.CustomerId}");
            }

            if (!cart.Items.Any())
            {
                throw new Exception("ShoppingCart is empty. Cannot create an Order.");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,  
                TotalAmount = 0             
            };

            _context.Orders.Add(order);

            decimal total = 0;

            foreach (var cartItem in cart.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId, cancellationToken);

                if (product == null)
                {
                    throw new Exception($"Product not found (ID = {cartItem.ProductId})");
                }

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    Price = product.Price
                };

                _context.OrderItems.Add(orderItem);

                total += product.Price * cartItem.Quantity;

                _context.ShoppingCartItems.Remove(cartItem);
            }

            order.TotalAmount = total;

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
