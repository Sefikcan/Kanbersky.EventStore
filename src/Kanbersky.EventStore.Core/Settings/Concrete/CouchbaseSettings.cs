using Kanbersky.EventStore.Core.Settings.Abstract;

namespace Kanbersky.EventStore.Core.Settings.Concrete
{
    public class CouchbaseSettings : ISettings
    {
        public string ConnectionStrings { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
