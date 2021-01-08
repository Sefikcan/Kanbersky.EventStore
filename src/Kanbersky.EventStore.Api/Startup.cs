using FluentValidation.AspNetCore;
using Kanbersky.EventStore.Core.Extensions;
using Kanbersky.EventStore.Infrastructure.Extensions;
using Kanbersky.EventStore.Services.Abstract;
using Kanbersky.EventStore.Services.Commands;
using Kanbersky.EventStore.Services.Concrete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                .AddSwagger(Configuration)
                .AddCore();

            services.AddInfrastructure(Configuration);

            services.AddMediatR(typeof(CreateTaskRequest));

            services.AddScoped<ITaskContentAggregate, TaskContentAggregate>();

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