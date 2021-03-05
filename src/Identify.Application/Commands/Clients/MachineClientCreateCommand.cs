using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Identify.Application.Commands.Clients
{
    public class MachineClientCreateCommandResult
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public List<string> GrantTypes { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> Scopes { get; set; }
    }

    public class MachineClientCreateCommand: IRequest<MachineClientCreateCommandResult>
    {
        public string Name { get; set; }
        public List<string> AllowedScopes { get; set; } = new List<string>();
    }

    public class MachineClientCreateCommandHandler : IRequestHandler<MachineClientCreateCommand, MachineClientCreateCommandResult>
    {
        private readonly ConfigurationDbContext _context;

        public MachineClientCreateCommandHandler(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<MachineClientCreateCommandResult> Handle(MachineClientCreateCommand request, CancellationToken cancellationToken)
        {
            var client = new IdentityServer4.EntityFramework.Entities.Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = request.Name,
                AllowedGrantTypes = new List<ClientGrantType>
                    {
                        new ClientGrantType
                        {
                            GrantType = IdentityServer4.Models.GrantType.ClientCredentials
                        }
                    },
                AllowedScopes = request.AllowedScopes.Select(x => new ClientScope
                {
                    Scope = x
                }).ToList(),
                ClientSecrets = new List<ClientSecret>
                    {
                        new ClientSecret
                        {
                            Expiration = DateTime.UtcNow.AddYears(2),
                            Description = "Example Secret",
                            Value = "thesecret".Sha256(),
                            Created = DateTime.UtcNow,
                            Type = IdentityServerConstants.SecretTypes.SharedSecret
                        }
                    },
                RequirePkce = false,
                RequireClientSecret = true
            };



            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new MachineClientCreateCommandResult
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
