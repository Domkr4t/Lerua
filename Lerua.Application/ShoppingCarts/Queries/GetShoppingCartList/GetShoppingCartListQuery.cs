using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.ShoppingCarts.Queries.GetShoppingCartList
{
    public class GetShoppingCartListQuery : IRequest<List<ShoppingCartLookupDto>>
    {
    }
}
