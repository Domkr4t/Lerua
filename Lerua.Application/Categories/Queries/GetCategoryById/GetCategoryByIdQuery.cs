using MediatR;

namespace Lerua.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
