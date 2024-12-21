using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Suppliers.Queries.GetSupplierList
{
    public class GetSupplierListQueryHandler : IRequestHandler<GetSupplierListQuery, List<SupplierLookupDto>>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetSupplierListQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SupplierLookupDto>> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers
                .ProjectTo<SupplierLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}

