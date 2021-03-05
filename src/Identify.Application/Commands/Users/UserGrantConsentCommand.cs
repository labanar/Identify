using IdentityServer4.Models;
using IdentityServer4.Services;
using MediatR;
using Identify.Application.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Users
{
    public class UserGrantConsentCommandResult
    {
        public string RedirectUrl { get; set; }
    }

    public class UserGrantConsentCommand: IRequest<UserGrantConsentCommandResult>
    {
        public string ReturnUrl { get; set; }
    }

    public class UserGrantConsentRequestHandler : IRequestHandler<UserGrantConsentCommand, UserGrantConsentCommandResult>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public UserGrantConsentRequestHandler(IIdentityServerInteractionService interaction, IEventService events)
        {
            _interaction = interaction;
            _events = events;
        }

        public async Task<UserGrantConsentCommandResult> Handle(UserGrantConsentCommand request, CancellationToken cancellationToken)
        {
            var decodedUrl = WebUtility.UrlDecode(WebUtility.UrlDecode(request.ReturnUrl));
            if (!_interaction.IsValidReturnUrl(decodedUrl))
                throw new UserGrantConsentException($"Invalid {nameof(UserGrantConsentCommand.ReturnUrl)}"); 

            var context = await _interaction.GetAuthorizationContextAsync(decodedUrl);

            await _interaction.GrantConsentAsync(context, new ConsentResponse
            {
                RememberConsent = true,
                ScopesValuesConsented = context.ValidatedResources.RawScopeValues 
            });

            return new UserGrantConsentCommandResult
            {
                RedirectUrl = decodedUrl
            };
        }
    }
}
