using Kanbersky.EventStore.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyCreatedObjectResult<T> : ObjectResult, IActionResult
    {
        public KanberskyCreatedObjectResult(T result) : base(new BaseApiResponseModel<T>(result))
        {
            StatusCode = StatusCodes.Status201Created;
        }
    }
}
