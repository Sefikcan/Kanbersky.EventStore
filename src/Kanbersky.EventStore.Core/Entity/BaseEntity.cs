namespace Kanbersky.EventStore.Core.Entity
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = "";

        public ulong Cas { get; set; } //version gibi düşünebilirsin.
    }
}
