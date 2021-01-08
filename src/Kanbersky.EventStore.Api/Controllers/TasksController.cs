using Kanbersky.EventStore.Core.Results.ApiResponses.Concrete;
using Kanbersky.EventStore.Services.Commands;
using Kanbersky.EventStore.Services.DTO.Request;
using Kanbersky.EventStore.Services.DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Api.Controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TasksController : KanberskyControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Task Operation
        /// </summary>
        /// <param name="createTaskRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskResponseModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequestModel createTaskRequest)
        {
            var response = await _mediator.Send(new CreateTaskRequest(createTaskRequest));
            return ApiOk(response);
        }
    }
}
