using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.OrderItems.Queries.GetOrderItemList
{
    public class GetOrderItemListQuery : IRequest<List<OrderItemLookupDto>>
    {
    }
}
