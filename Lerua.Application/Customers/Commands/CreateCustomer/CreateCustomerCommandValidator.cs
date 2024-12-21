using FluentValidation;

namespace Lerua.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.FullName).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Email).NotEmpty().MaximumLength(100);
            RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(15);
        }
    }
}
