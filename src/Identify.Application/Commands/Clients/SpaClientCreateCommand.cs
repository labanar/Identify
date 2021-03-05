using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Clients
{
    public class SpaClientCreateCommandResult
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public List<string> GrantTypes { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> Scopes { get; set; }
    }

    public class SpaClientCreateCommand: IRequest<SpaClientCreateCommandResult>
    {
        public string Name { get; set; }
        public List<string> RedirectUris { get; set; } = new List<string>();
        public List<string> AllowedScopes { get; set; } = new List<string>();
    }

    public class SpaClientCreateRequestHandler : IRequestHandler<SpaClientCreateCommand, SpaClientCreateCommandResult>
    {
        private readonly ConfigurationDbContext _context;

        public SpaClientCreateRequestHandler(ConfigurationDbContext context)
        {
            _context = context;
        }


        public async Task<SpaClientCreateCommandResult> Handle(SpaClientCreateCommand request, CancellationToken cancellationToken)
        {
            var client = new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = request.Name,
                AllowedGrantTypes = new List<ClientGrantType>
                    {
                        new ClientGrantType
                        {
                            GrantType = IdentityServer4.Models.GrantType.AuthorizationCode
                        }
                    },
                RedirectUris = request.RedirectUris.Select(x => new ClientRedirectUri
                {
                    RedirectUri = x
                }).ToList(),
                AllowedScopes = request.AllowedScopes.Select(x => new ClientScope
                {
                    Scope = x,
                }).ToList(),
                ClientSecrets = new List<ClientSecret>(),
                RequirePkce = false,
                RequireClientSecret = false
            };

            if (!client.AllowedScopes.Any(x => x.Scope == IdentityServerConstants.StandardScopes.OpenId))
                client.AllowedScopes.Add(new ClientScope
                {
                    Scope = IdentityServerConstants.StandardScopes.OpenId
                });
            if (!client.AllowedScopes.Any(x => x.Scope == IdentityServerConstants.StandardScopes.Email))
                client.AllowedScopes.Add(new ClientScope
                {
                    Scope = IdentityServerConstants.StandardScopes.Email
                });
            if (!client.AllowedScopes.Any(x => x.Scope == IdentityServerConstants.StandardScopes.Profile))
                client.AllowedScopes.Add(new ClientScope
                {
                    Scope = IdentityServerConstants.StandardScopes.Profile
                });

            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new SpaClientCreateCommandResult
            {
                ClientId = client.ClientId,
                Name = client.ClientName,
                GrantTypes = client.AllowedGrantTypes.Select(x => x.GrantType).ToList(),
                RedirectUris = client.RedirectUris.Select(x => x.RedirectUri).ToList(),
                Scopes = client.AllowedScopes.Select(x => x.Scope).ToList()
            };
        }
    }
}
