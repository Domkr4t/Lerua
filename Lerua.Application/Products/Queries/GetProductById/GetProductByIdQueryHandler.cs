using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Lerua.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler
        : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new Exception($"Product with Id={request.Id} not found");
            }

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}
