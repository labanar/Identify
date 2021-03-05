using MediatR;
using Microsoft.EntityFrameworkCore;
using Identify.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserPasswordResetCommand : IRequest<Unit>
    {
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }

    public class UserPasswordResetRequestHandler : IRequestHandler<UserPasswordResetCommand, Unit>
    {
        private readonly IIdentityDbContext _context;
        private readonly IHashingService _hashingService;

        public UserPasswordResetRequestHandler(IIdentityDbContext context, IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        public async Task<Unit> Handle(UserPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var resetToken = await _context
                .PasswordResetTokens
                .Include(x => x.Token)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token.Value == request.ResetToken);

            if (resetToken == default)
                throw new UserPasswordResetException("Reset token cannot be found.");

            if (DateTime.UtcNow > resetToken.Token.DateExpires)
                throw new UserPasswordResetException("Reset token has expired.");

            //Generate the new hashed password and update the user entity
            var hashedPassword = await _hashingService.CreateHash(request.Password);
            resetToken.User.HashedPassword = hashedPassword;
            resetToken.Token.Consumed = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
