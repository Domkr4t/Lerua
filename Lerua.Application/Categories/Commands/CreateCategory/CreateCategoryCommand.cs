using MediatR;
using System;

namespace Lerua.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
