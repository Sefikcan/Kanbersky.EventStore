using Kanbersky.EventStore.Core.Settings.Abstract;

namespace Kanbersky.EventStore.Core.Settings.Concrete
{
    public class EventStoreSettings : ISettings
    {
        public string ConnectionString { get; set; }

        public string ConnectionName { get; set; }
    }
}
