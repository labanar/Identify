using IdentityServer4.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Identify.Application.Exceptions;
using Identify.Application.Password;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
using System;
using Microsoft.Extensions.Options;

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
        public bool RememberMe { get; set; }
    }


    public class UserAuthenticateRequestHandler : IRequestHandler<UserAuthenticateCommand, UserAuthenticateCommandResult>
    {
        private readonly IIdentityDbContext _context;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long _rememberMeDurationMinutes;

        public UserAuthenticateRequestHandler(IIdentityDbContext context, IPasswordValidator passwordValidator, IIdentityServerInteractionService interaction, IHttpContextAccessor httpContextAccessor, IOptions<IdentityServerOptions> options)
        {
            _context = context;
            _passwordValidator = passwordValidator;
            _interaction = interaction;
            _httpContextAccessor = httpContextAccessor;
            _rememberMeDurationMinutes = options.Value.RememberMeDurationMinutes;
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

            var idSrvAuthProps = new AuthenticationProperties
            {
                IsPersistent = request.RememberMe,
                ExpiresUtc = request.RememberMe ? DateTime.UtcNow.AddMinutes(_rememberMeDurationMinutes) : default
            };

            var idSrvUser = new IdentityServerUser(user.Id.ToString())
            {
                DisplayName = user.Username
            };

            await _httpContextAccessor.HttpContext.SignInAsync(idSrvUser, idSrvAuthProps);
            return new UserAuthenticateCommandResult
            {
                UserId = user.Id.ToString(),
                Username = user.Username,
                RedirectUrl = decodedUrl
            };
        }
    }
}
