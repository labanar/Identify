using Identify.Application.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identify.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateUser([FromBody] UserActivateCommand request) =>
            await HandleActionRequest(request);

        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] UserPasswordResetCommand request) =>
            await HandleActionRequest(request);

        [HttpPost("password/forgot")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] UserPasswordResetRequestCommand request) =>
            await HandleActionRequest(request);

    }
}
