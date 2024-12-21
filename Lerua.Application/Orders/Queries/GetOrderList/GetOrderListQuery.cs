using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<List<OrderLookupDto>>
    {
    }
}
