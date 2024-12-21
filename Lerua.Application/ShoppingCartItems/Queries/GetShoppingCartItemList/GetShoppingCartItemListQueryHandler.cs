using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemList
{
    public class GetShoppingCartItemListQueryHandler
        : IRequestHandler<GetShoppingCartItemListQuery, List<ShoppingCartItemLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetShoppingCartItemListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShoppingCartItemLookupDto>> Handle(
            GetShoppingCartItemListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.ShoppingCartItems
                .ProjectTo<ShoppingCartItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
