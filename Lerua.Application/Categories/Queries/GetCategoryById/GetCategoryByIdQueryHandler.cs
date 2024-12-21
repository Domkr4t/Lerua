using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Lerua.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQuery, CategoryDetailsDto>
    {
        private readonly ILeruaDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ILeruaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDetailsDto> Handle(GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                // Можно бросить кастомное исключение NotFoundException, 
                // либо вернуть null (а потом на уровне контроллера выдать 404).
                throw new Exception($"Category with Id={request.Id} not found");
            }

            return _mapper.Map<CategoryDetailsDto>(category);
        }
    }
}
