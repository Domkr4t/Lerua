using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Lerua.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDto = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Id == request.Id)
                .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (orderDto == null)
            {
                throw new Exception($"Order {request.Id} not found");
            }

            return orderDto;
        }
    }

}
