using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Clients
{
    public class ClientSecretGenerateCommandResult
    {
        public string Secret { get; set; }
    }

    public class ClientSecretGenerateCommand: IRequest<ClientSecretGenerateCommandResult>
    {
        public string ClientId { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }

    public class ClientSecretGenerateCommandHandler : IRequestHandler<ClientSecretGenerateCommand, ClientSecretGenerateCommandResult>
    {
        private readonly ConfigurationDbContext _context;
        public ClientSecretGenerateCommandHandler(ConfigurationDbContext context)
        {
            _context = context;
        }


        public async Task<ClientSecretGenerateCommandResult> Handle(ClientSecretGenerateCommand request, CancellationToken cancellationToken)
        {
            var secret = Guid.NewGuid().ToString().Replace("-", "");

            var clientSecret = new ClientSecret
            {
                Value = secret,
                Description = request.Description,
                Created = DateTime.UtcNow,
                Expiration = request.ExpirationDateUtc
            };

            var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId.Equals(request.ClientId, StringComparison.OrdinalIgnoreCase));
            if (client == default)
                throw new ApplicationException($"Client with the ClientId [{request.ClientId}] cannot be found.");

            client.ClientSecrets.Add(clientSecret);
            await _context.SaveChangesAsync(cancellationToken);


            return new ClientSecretGenerateCommandResult
            {
                Secret = secret
            };
        }
    }
}
