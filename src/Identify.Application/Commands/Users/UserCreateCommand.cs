using MediatR;
using Identify.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserCreateCommandResult
    {
        public int UserId { get; set; }
        public string ActivationToken { get; set; }
    }

    public class UserCreateCommand : IRequest<UserCreateCommandResult>
    {
        public string Email { get; set; }
    }

    public class UserCreateRequestHandler : IRequestHandler<UserCreateCommand, UserCreateCommandResult>
    {
        private readonly IIdentityDbContext _context;
        private readonly IEmailService _emailService;

        public UserCreateRequestHandler(IIdentityDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<UserCreateCommandResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var activationToken = Guid.NewGuid().ToString();
            var user = new User
            {
                Active = false,
                DateCreated = DateTime.UtcNow,
                DateLastModified = DateTime.UtcNow,
                EmailAddress = request.Email,
                ActivationTokens = new List<UserActivationToken>
                {
                    new UserActivationToken
                    {
                        Token = new OneTimeUseToken
                        {
                            Value = activationToken,
                            DateCreated = DateTime.UtcNow,
                            DateExpires = DateTime.UtcNow.AddHours(24),
                            Consumed = false
                        }
                    }
                }
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
            await _emailService.SendWelcomeEmail(request.Email, activationToken);

            return new UserCreateCommandResult
            {
                UserId = user.Id,
                ActivationToken = activationToken
            };
        }
    }
}
