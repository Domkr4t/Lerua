using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Lerua.Application.ShoppingCartItems.Queries.GetShoppingCartItemsByCartId
{
    public class GetShoppingCartItemsByCartIdQueryHandler
        : IRequestHandler<GetShoppingCartItemsByCartIdQuery, List<ShoppingCartItemLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetShoppingCartItemsByCartIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShoppingCartItemLookupDto>> Handle(GetShoppingCartItemsByCartIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.ShoppingCartItems
                .Where(item => item.ShoppingCartId == request.CartId)
                .ProjectTo<ShoppingCartItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
