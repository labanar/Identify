using IdentityServer4.EntityFramework.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Queries
{
    public class ConsentSummary   
    {
        public string ClientName { get; set; }
        public List<Scope> Scopes { get; set; } = new List<Scope>();
        public class Scope
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
        }
    }

    public class ConsentSummaryQuery: IRequest<ConsentSummary>
    {
        public string ClientId { get; set; }
        public List<string> Scopes { get; set; }
    }

    public class ConsentSummaryQueryHandler : IRequestHandler<ConsentSummaryQuery, ConsentSummary>
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        public ConsentSummaryQueryHandler(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }


        public async Task<ConsentSummary> Handle(ConsentSummaryQuery request, CancellationToken cancellationToken)
        {
            var apiScopes = await _configurationDbContext.ApiScopes
                .Where(x => request.Scopes.Contains(x.Name))
                .ToListAsync();

            var client =
                await _configurationDbContext.Clients
                    .FirstOrDefaultAsync(x => x.ClientId == request.ClientId);

            //TODO - remove this delay after testing the Skeleton UI
            await Task.Delay(2500);

            var summary = new ConsentSummary();
            summary.ClientName = client?.ClientName;
            foreach (var scope in apiScopes)
            {
                summary.Scopes.Add(new ConsentSummary.Scope
                {
                    Name = scope.Name,
                    Description = scope.Description,
                    DisplayName = scope.DisplayName
                });
            }

            return summary;
        }
    }
}
