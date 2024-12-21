using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.ShoppingCarts.Queries.GetShoppingCartList
{
    public class GetShoppingCartListQueryHandler
    : IRequestHandler<GetShoppingCartListQuery, List<ShoppingCartLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetShoppingCartListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShoppingCartLookupDto>> Handle(GetShoppingCartListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.ShoppingCarts
                .Include(sc => sc.Items)
                .ProjectTo<ShoppingCartLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

}
