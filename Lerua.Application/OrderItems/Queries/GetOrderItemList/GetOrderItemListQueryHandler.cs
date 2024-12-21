using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.OrderItems.Queries.GetOrderItemList
{
    public class GetOrderItemListQueryHandler : IRequestHandler<GetOrderItemListQuery, List<OrderItemLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderItemListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderItemLookupDto>> Handle(GetOrderItemListQuery request, CancellationToken cancellationToken)
        {
            return await _context.OrderItems
                .ProjectTo<OrderItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
