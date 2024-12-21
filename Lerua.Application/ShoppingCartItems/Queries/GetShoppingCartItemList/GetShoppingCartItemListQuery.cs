using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemList
{
    public class GetShoppingCartItemListQuery : IRequest<List<ShoppingCartItemLookupDto>>
    {
    }
}
