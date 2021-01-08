using Kanbersky.EventStore.Services.Commands;
using Kanbersky.EventStore.Services.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Api.Controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequestModel createTaskRequest)
        {
            var response = await _mediator.Send(new CreateTaskRequest(createTaskRequest));
            return Ok(response);
        }
    }
}
