using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryLookupDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
