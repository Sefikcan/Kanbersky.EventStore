using Kanbersky.EventStore.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyUpdatedObjectResult<T> : ObjectResult, IActionResult
    {
        public KanberskyUpdatedObjectResult(T result) : base(new BaseApiResponseModel<T>(result))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
