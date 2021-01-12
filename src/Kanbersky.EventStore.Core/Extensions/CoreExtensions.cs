using Kanbersky.EventStore.Core.EventStores.Repositories.Abstract;
using Kanbersky.EventStore.Core.EventStores.Repositories.Concrete;
using Kanbersky.EventStore.Core.Messaging.Abstract;
using Kanbersky.EventStore.Core.Messaging.Concrete.RabbitMQ;
using Kanbersky.EventStore.Core.Pipelines;
using Kanbersky.EventStore.Core.Settings.Concrete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.EventStore.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelines<,>));

            services.AddHealthChecks();

            services.AddScoped(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));

            var massTransitSettings = new MassTransitSettings();
            configuration.GetSection(nameof(MassTransitSettings)).Bind(massTransitSettings);
            services.Configure<MassTransitSettings>(configuration.GetSection(nameof(MassTransitSettings)));
            services.AddSingleton(massTransitSettings);

            services.AddScoped<IEventListener, RabbitMQListener>();

            return services;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            return app;
        }
    }
}
