using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyNoContentResult : StatusCodeResult, IActionResult
    {
        public KanberskyNoContentResult() : base(StatusCodes.Status204NoContent)
        {
        }
    }
}
