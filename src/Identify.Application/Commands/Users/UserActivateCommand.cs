using MediatR;
using Microsoft.EntityFrameworkCore;
using Identify.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserActivateCommand: IRequest<Unit>
    {
        public string ActivationToken { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }

    public class UserActivateRequestHandler : IRequestHandler<UserActivateCommand, Unit>
    {
        private readonly IIdentityDbContext _context;
        private readonly IHashingService _hashingService;

        public UserActivateRequestHandler(IIdentityDbContext context, IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        public async Task<Unit> Handle(UserActivateCommand request, CancellationToken cancellationToken)
        {
            var activationToken = await _context
                .ActivationTokens
                .Include(x => x.Token)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token.Value == request.ActivationToken);

            if(activationToken == default)
                throw new UserActivationException("Activation token cannot be found.");
            
            if(activationToken.User.Active)
                throw new UserActivationException("User has already activated.");

            if (DateTime.UtcNow > activationToken.Token.DateExpires)
                throw new UserActivationException("Activation token is expired.");

            //Generate the new hashed password and update the user entity
            var hashedPassword = await _hashingService.CreateHash(request.Password);
            activationToken.User.Username = request.Username;
            activationToken.User.HashedPassword = hashedPassword;
            activationToken.User.Active = true;
            activationToken.User.IsEmailVerified = true;
            activationToken.Token.Consumed = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
