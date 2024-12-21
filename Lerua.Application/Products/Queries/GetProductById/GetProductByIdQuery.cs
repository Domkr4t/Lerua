using MediatR;

namespace Lerua.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
