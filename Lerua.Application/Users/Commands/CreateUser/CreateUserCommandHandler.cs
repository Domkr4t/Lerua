using Lerua.Application.Interfaces;
using Lerua.Domain;
using MediatR;

namespace Lerua.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly ILeruaDbContext _context;

        public CreateUserCommandHandler(ILeruaDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                Role = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
