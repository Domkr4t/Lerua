using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderLookupDto>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

}
