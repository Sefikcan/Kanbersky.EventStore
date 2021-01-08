using Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract;
using System;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.EventStores.Repositories.Abstract
{
    public interface IAggregateRepository<T> where T : IAggregate, new()
    {
        Task SaveAsync(T aggregate);
        Task<T> LoadAsync(string aggregateId);
    }
}
