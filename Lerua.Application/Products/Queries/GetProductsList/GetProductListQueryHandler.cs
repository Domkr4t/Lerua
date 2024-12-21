using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Products.Queries.GetProductsList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<ProductLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductLookupDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
   
    }
}
