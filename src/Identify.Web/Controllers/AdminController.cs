using Identify.Application.Commands.Clients;
using Identify.Application.Commands.Resources;
using Identify.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identify.Web.Controllers
{

    /// <summary>
    /// For obvious reasons you should add an authorization policy to this controller. These actions should only
    /// be executed by an administrator.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ApiControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand request) =>
           await HandleDataRequest<UserCreateCommand, UserCreateCommandResult>(request);

        [HttpPost("resource")]
        public async Task<IActionResult> CreateApiResource([FromBody] ApiResourceCreateCommand request) =>
            await HandleDataRequest<ApiResourceCreateCommand, ApiResourceCreateCommandResult>(request);

        [HttpPost("client/spa")]
        public async Task<IActionResult> CreateSpaClient([FromBody] SpaClientCreateCommand request) =>
            await HandleDataRequest<SpaClientCreateCommand, SpaClientCreateCommandResult>(request);

        [HttpPost("client/machine")]
        public async Task<IActionResult> CreateMachineClient([FromBody] MachineClientCreateCommand request) =>
            await HandleDataRequest<MachineClientCreateCommand, MachineClientCreateCommandResult>(request);

        [HttpPost("client/secret")]
        public async Task<IActionResult> GenerateClientSecret([FromBody] ClientSecretGenerateCommand request) =>
            await HandleDataRequest<ClientSecretGenerateCommand, ClientSecretGenerateCommandResult>(request);
    }
}
