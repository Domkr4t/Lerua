using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Lerua.Application.OrderItems.Queries.GetOrderItemsByOrderId
{
    public class GetOrderItemsByOrderIdQueryHandler
        : IRequestHandler<GetOrderItemsByOrderIdQuery, List<OrderItemLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderItemsByOrderIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderItemLookupDto>> Handle(GetOrderItemsByOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == request.OrderId)
                .ProjectTo<OrderItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
