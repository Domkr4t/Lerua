using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Lerua.Application.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQueryHandler
        : IRequestHandler<GetSupplierByIdQuery, SupplierDetailsDto>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupplierDetailsDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (supplier == null)
            {
                throw new Exception($"Supplier with Id={request.Id} not found");
            }

            return _mapper.Map<SupplierDetailsDto>(supplier);
        }
    }
}

