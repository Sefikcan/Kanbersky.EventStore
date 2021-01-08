using System;
using System.Collections.Generic;

namespace Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract
{
    public interface IAggregate
    {
        Guid Id { get; }

        long Version { get; }

        DateTime CreatedDate { get; }

        void Apply(object @event);

        void Load(long version, IEnumerable<object> histories);

        object[] GetChanges();
    }
}
