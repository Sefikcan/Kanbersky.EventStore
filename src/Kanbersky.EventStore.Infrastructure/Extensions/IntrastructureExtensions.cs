using Couchbase.Extensions.DependencyInjection;
using EventStore.ClientAPI;
using Kanbersky.EventStore.Core.Settings.Concrete;
using Kanbersky.EventStore.Infrastructure.Abstract;
using Kanbersky.EventStore.Infrastructure.Concrete;
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

            var couchbaseSettings = new CouchbaseSettings();
            configuration.GetSection(nameof(CouchbaseSettings)).Bind(couchbaseSettings);
            services.Configure<CouchbaseSettings>(configuration.GetSection(nameof(CouchbaseSettings)));

            services.AddCouchbase((opt) =>
            {
                opt.ConnectionString = couchbaseSettings.ConnectionStrings;
                opt.Username = couchbaseSettings.UserName;
                opt.Password = couchbaseSettings.Password;
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
