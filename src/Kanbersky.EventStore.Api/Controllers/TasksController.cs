using Kanbersky.EventStore.Core.Results.ApiResponses.Concrete;
using Kanbersky.EventStore.Services.Commands;
using Kanbersky.EventStore.Services.DTO.Request;
using Kanbersky.EventStore.Services.DTO.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
            return ApiCreated(response);
        }

        /// <summary>
        /// Assign Task Operation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assignRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(AssignTaskResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Assign([FromRoute] Guid id, [FromBody] AssignTaskRequestModel assignRequest)
        {
            var response = await _mediator.Send(new AssignTaskRequest(id, assignRequest));
            return ApiUpdated(response);
        }

        /// <summary>
        /// Change Status Operation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeTaskStatusRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/change-status")]
        [ProducesResponseType(typeof(ChangeTaskStatusResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeTaskStatus([FromRoute] Guid id, [FromBody] ChangeTaskStatusRequestModel changeTaskStatusRequest)
        {
            var response = await _mediator.Send(new ChangeTaskStatusRequest(id, changeTaskStatusRequest));
            return ApiUpdated(response);
        }

        /// <summary>
        /// Get By Id operation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return ApiOk();
        }
    }
}
