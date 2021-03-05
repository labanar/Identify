using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserPasswordResetRequestCommand: IRequest<Unit>
    {
        public string Email { get; set; }
    }

    public class UserPasswordResetRequestCommandHandler : IRequestHandler<UserPasswordResetRequestCommand, Unit>
    {
        private readonly IIdentityDbContext _context;
        private readonly IEmailService _emailService;

        public UserPasswordResetRequestCommandHandler(IIdentityDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(UserPasswordResetRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == request.Email);
            if (user == default)
                return Unit.Value;

            var token = Guid.NewGuid().ToString();
            user.PasswordResetTokens.Add(new Entities.UserPasswordResetToken
            {
                Token = new Entities.OneTimeUseToken
                {
                    Value = token,
                    DateCreated = DateTime.UtcNow,
                    DateExpires = DateTime.UtcNow.AddHours(24),
                    Consumed = false
                }
            });

            await _context.SaveChangesAsync(cancellationToken);
            await _emailService.SendPasswordResetEmail(user.EmailAddress, token);
            return Unit.Value;
        }
    }
}
