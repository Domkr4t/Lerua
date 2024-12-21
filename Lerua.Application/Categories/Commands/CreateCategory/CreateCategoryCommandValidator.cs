using FluentValidation;

namespace Lerua.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
            RuleFor(c => c.Description).NotEmpty().MaximumLength(200);
        }
    }
}
