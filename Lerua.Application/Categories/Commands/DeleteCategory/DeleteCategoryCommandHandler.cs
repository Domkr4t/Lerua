using Lerua.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lerua.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ILeruaDbContext _context;

        public DeleteCategoryCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception($"Category (Id={request.Id}) not found.");
            }

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
