using MediatR;
using System.Collections.Generic;

namespace Lerua.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<List<CategoryLookupDto>>
    {
    }
}
