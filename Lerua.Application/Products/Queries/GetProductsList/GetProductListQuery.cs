using MediatR;

namespace Lerua.Application.Products.Queries.GetProductsList
{
    public class GetProductListQuery : IRequest<List<ProductLookupDto>>
    {
    }
}
