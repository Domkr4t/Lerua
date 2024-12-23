using AutoMapper;
using Lerua.Application.Categories.Queries.GetCategoryById;
using Lerua.Application.Categories.Queries.GetCategoryList;
using Lerua.Domain;

namespace Lerua.Tests.Common
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Category, CategoryDetailsDto>();
            CreateMap<Category, CategoryLookupDto>();
        }
    }
}
