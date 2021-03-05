using Identify.Web.Api.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identify.Web.Controllers
{
    public abstract class ApiControllerBase: ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected async Task<IActionResult> HandleActionRequest<T>(T request) where T : IRequest<Unit>
        {
            await _mediator.Send(request);
            return new JsonResult(new ActionResponse { Success = true });
        }

        protected async Task<IActionResult> HandleDataRequest<T, TData>(T request) where T : IRequest<TData>
        {
            var data = await _mediator.Send(request);
            return new JsonResult(new DataResponse<TData>(data));
        }
    }
}
