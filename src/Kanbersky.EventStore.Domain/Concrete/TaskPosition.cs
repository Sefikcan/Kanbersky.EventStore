using EventStore.ClientAPI;
using Kanbersky.EventStore.Core.Entity;

namespace Kanbersky.EventStore.Domain.Concrete
{
    /// <summary>
    /// Event Store'da tutulan son pozisyonu almak için kullanılır.
    /// </summary>
    public class TaskPosition : BaseEntity, IEntity
    {
        public string Key { get; set; }

        public Position Position { get; set; }
    }
}
