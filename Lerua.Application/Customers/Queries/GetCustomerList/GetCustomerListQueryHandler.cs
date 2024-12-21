using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Customers.Queries.GetCustomerList
{
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, List<CustomerLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerLookupDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .ProjectTo<CustomerLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
