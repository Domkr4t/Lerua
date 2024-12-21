using FluentValidation;

namespace Lerua.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ContactInfo).NotEmpty().MaximumLength(200);
        }
    }
}
