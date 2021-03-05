using Identify.Application.Commands.Users;
using Identify.Application.Queries;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Identify.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ApiControllerBase
    {
        public AuthenticateController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserAuthenticateCommand request)
        {
            var result = await _mediator.Send(request);
            await Request.HttpContext.SignInAsync(new IdentityServerUser(result.UserId) { DisplayName = result.Username });
            return new JsonResult(result);
        }

        [HttpGet("consent")]
        public async Task<IActionResult> GetConsentSummary([FromQuery] string scopes, [FromQuery] string clientId)
        {
            var request = new ConsentSummaryQuery
            {
                ClientId = clientId,
                Scopes = scopes.Split(',').ToList()
            };

            return await HandleDataRequest<ConsentSummaryQuery, ConsentSummary>(request);
        }

        [HttpPost("consent")]
        public async Task<IActionResult> GrantConsent([FromBody] UserGrantConsentCommand request) =>
            await HandleDataRequest<UserGrantConsentCommand, UserGrantConsentCommandResult>(request);
                
    }
}
