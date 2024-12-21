using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lerua.Application.Interfaces;
using Lerua.Application.ShoppingCarts.Queries.GetShoppingCartById;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetShoppingCartByIdQueryHandler
    : IRequestHandler<GetShoppingCartByIdQuery, ShoppingCartDetailsDto>
{
    private readonly ILeruaDbContext _context;
    private readonly IMapper _mapper;

    public GetShoppingCartByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShoppingCartDetailsDto> Handle(GetShoppingCartByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Важно .Include(sc => sc.Items)
        var cart = await _context.ShoppingCarts
            .Include(sc => sc.Items)
            .Where(sc => sc.Id == request.Id)
            .ProjectTo<ShoppingCartDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart == null)
        {
            throw new Exception($"Cart {request.Id} not found");
        }

        return cart;
    }
}
