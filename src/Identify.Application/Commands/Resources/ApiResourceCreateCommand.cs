using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identify.Application.Commands.Resources
{
    public class ApiResourceCreateCommandResult
    {
        public string ResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Scope> Scopes { get; set; } = new List<Scope>();

        public class Scope
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

    public class ApiResourceCreateCommand : IRequest<ApiResourceCreateCommandResult>
    {
        public string Name { get; set; }
        public List<Scope> Scopes { get; set; } = new List<Scope>();

        public class Scope
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string DisplayName { get; set; }
        }
    }

    public class ApiResourceCreateRequestHandler : IRequestHandler<ApiResourceCreateCommand, ApiResourceCreateCommandResult>
    {
        private readonly ConfigurationDbContext _context;

        public ApiResourceCreateRequestHandler(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResourceCreateCommandResult> Handle(ApiResourceCreateCommand request, CancellationToken cancellationToken)
        {
            var apiResource = new ApiResource
            {
                Name = request.Name,
                DisplayName = request.Name,
                Enabled = true,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow,
                Description = request.Name
            };

            var scopes = request.Scopes.Select(x => new ApiScope
            {
                Name = GenerateScopeName(request.Name, x.Name),
                Description = x.Description,
                DisplayName = x.DisplayName
            }).ToList();

            await _context.ApiResources.AddAsync(apiResource, cancellationToken);
            await _context.ApiScopes.AddRangeAsync(scopes, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResourceCreateCommandResult
            {
                ResourceId = apiResource.Id.ToString(),
                Description = apiResource.Description,
                Name = apiResource.Name,
                Scopes = scopes.Select(x => new ApiResourceCreateCommandResult.Scope
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList()
            };
        }

        private static string GenerateScopeName(string apiName, string scopeName)
        {
            //parse out the characters in the name

            var apiNameNormalized = new StringBuilder();
            foreach (var c in apiName)
            {
                if (char.IsLetterOrDigit(c))
                    apiNameNormalized.Append(c);
            }

            return $"{apiNameNormalized}:{scopeName}";
        }
    }
}
