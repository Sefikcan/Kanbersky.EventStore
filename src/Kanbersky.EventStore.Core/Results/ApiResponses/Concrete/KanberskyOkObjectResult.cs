using Kanbersky.EventStore.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyOkObjectResult<T> : ObjectResult, IActionResult
    {
        public KanberskyOkObjectResult(T result) : base(new BaseApiResponseModel<T>(result))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
