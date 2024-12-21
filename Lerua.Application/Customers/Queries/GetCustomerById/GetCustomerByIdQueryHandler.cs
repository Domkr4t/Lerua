using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Lerua.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler
        : IRequestHandler<GetCustomerByIdQuery, CustomerDetailsDto>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDetailsDto> Handle(GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
                throw new Exception($"Customer with Id={request.Id} not found");

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}
