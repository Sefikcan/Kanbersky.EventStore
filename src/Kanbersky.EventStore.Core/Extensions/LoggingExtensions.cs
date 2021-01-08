using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using System.Linq;

namespace Kanbersky.EventStore.Core.Extensions
{
    public static class LoggingExtensions
    {
        public static Serilog.Core.Logger AddLogging(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationIdHeader()
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return logger;
        }

        public static IApplicationBuilder UseLogging(this IApplicationBuilder app, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            app.UseAllElasticApm(configuration);
            loggerFactory.AddSerilog();

            return app;
        }
    }
}
