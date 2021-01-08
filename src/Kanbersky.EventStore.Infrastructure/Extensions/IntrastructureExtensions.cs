using EventStore.ClientAPI;
using Kanbersky.EventStore.Core.Settings.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.EventStore.Infrastructure.Extensions
{
    public static class IntrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var eventStoreSettings = new EventStoreSettings();
            configuration.GetSection(nameof(EventStoreSettings)).Bind(eventStoreSettings);
            services.Configure<EventStoreSettings>(configuration.GetSection(nameof(EventStoreSettings)));

            var eventStoreConnection = EventStoreConnection.Create(
               connectionString: eventStoreSettings.ConnectionString,
               builder: ConnectionSettings.Create().KeepReconnecting(),
               connectionName: eventStoreSettings.ConnectionName);

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);

            return services;
        }
    }
}
