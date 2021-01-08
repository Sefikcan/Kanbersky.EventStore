using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyControllerBase : ControllerBase
    {
        [NonAction]
        public KanberskyCreatedObjectResult<TResult> ApiCreated<TResult>(TResult result) where TResult : class
        {
            return new KanberskyCreatedObjectResult<TResult>(result);
        }

        [NonAction]
        public KanberskyOkResult ApiOk()
        {
            return new KanberskyOkResult();
        }

        [NonAction]
        public KanberskyOkObjectResult<TResult> ApiOk<TResult>(TResult result) where TResult : class
        {
            return new KanberskyOkObjectResult<TResult>(result);
        }

        [NonAction]
        public KanberskyNoContentResult ApiNoContent()
        {
            return new KanberskyNoContentResult();
        }

        [NonAction]
        public KanberskyUpdatedObjectResult<TResult> ApiUpdated<TResult>(TResult result) where TResult : class
        {
            return new KanberskyUpdatedObjectResult<TResult>(result);
        }
    }
}
