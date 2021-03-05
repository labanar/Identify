using IdentityServer4.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Identify.Application.Exceptions;
using Identify.Application.Password;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserAuthenticateCommandResult
    {
        public string RedirectUrl { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }

    public class UserAuthenticateCommand: IRequest<UserAuthenticateCommandResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class UserAuthenticateRequestHandler : IRequestHandler<UserAuthenticateCommand, UserAuthenticateCommandResult>
    {
        private readonly IIdentityDbContext _context;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IIdentityServerInteractionService _interaction;

        public UserAuthenticateRequestHandler(IIdentityDbContext context, IPasswordValidator passwordValidator, IIdentityServerInteractionService interaction)
        {
            _context = context;
            _passwordValidator = passwordValidator;
            _interaction = interaction;
        }

        public async Task<UserAuthenticateCommandResult> Handle(UserAuthenticateCommand request, CancellationToken cancellationToken)
        {
            var decodedUrl = WebUtility.UrlDecode(WebUtility.UrlDecode(request.ReturnUrl));
            if(!_interaction.IsValidReturnUrl(decodedUrl))
                throw new UserAuthenticateException("Invalid returnUrl");
            
            var context = await _interaction.GetAuthorizationContextAsync(decodedUrl);

            //Validate the uer credentials
            if (!await _passwordValidator.ValidateAsync(request.Username, request.Password))
                throw new UserAuthenticateException("Invalid username or password.");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            return new UserAuthenticateCommandResult
            {
                UserId = user.Id.ToString(),
                Username = user.Username,
                RedirectUrl = decodedUrl
            };
        }
    }
}
