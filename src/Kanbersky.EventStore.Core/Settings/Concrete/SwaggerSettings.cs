using Kanbersky.EventStore.Core.Settings.Abstract;
using Microsoft.OpenApi.Models;

namespace Kanbersky.EventStore.Core.Settings.Concrete
{
    public class SwaggerSettings : OpenApiInfo, ISettings
    {
        public string VersionName { get; set; } = "v1";

        public string RoutePrefix { get; set; } = "";
    }
}
