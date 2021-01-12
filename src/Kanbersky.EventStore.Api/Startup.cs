using FluentValidation.AspNetCore;
using Kanbersky.EventStore.Core.Extensions;
using Kanbersky.EventStore.Infrastructure.Extensions;
using Kanbersky.EventStore.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Kanbersky.EventStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwagger(Configuration, Assembly.GetExecutingAssembly())
                .AddCore(Configuration);

            services.AddInfrastructure(Configuration);

            services.AddServiceLayer();

            services.AddControllers()
                .AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseLogging(Configuration, loggerFactory)
                .UseSwagger(Configuration)
                .UseCore();
        }
    }
}
