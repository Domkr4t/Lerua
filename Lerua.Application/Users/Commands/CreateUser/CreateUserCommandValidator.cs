using FluentValidation;

namespace Lerua.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Username).NotEmpty().MaximumLength(50);
            RuleFor(u => u.PasswordHash).NotEmpty().MaximumLength(256);
            RuleFor(u => u.Role).NotEmpty().MaximumLength(20);
        }
    }
}
