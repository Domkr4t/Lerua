using MediatR;
using System;

namespace Lerua.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
    }
}
