namespace Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract
{
    public abstract class BaseAggregate
    {
        protected abstract void When(object @event);
    }
}
