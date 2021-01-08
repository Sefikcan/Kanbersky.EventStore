using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.EventStore.Core.Results.ApiResponses.Concrete
{
    public class KanberskyOkResult : StatusCodeResult, IActionResult
    {
        public KanberskyOkResult() : base(StatusCodes.Status200OK)
        {
        }
    }
}
