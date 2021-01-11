using EventStore.ClientAPI;
using Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract;
using Kanbersky.EventStore.Core.EventStores.Repositories.Abstract;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.EventStores.Repositories.Concrete
{
    //TODO: Refactor
    public class AggregateRepository<T> : IAggregateRepository<T> where T : IAggregate, new()
    {
        private readonly IEventStoreConnection _eventStore;

        public AggregateRepository(IEventStoreConnection eventStore)
        {
            _eventStore = eventStore;
        }

        /// <summary>
        /// Event store'dan event alır
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        public async Task<T> LoadAsync(string aggregateId)
        {
            if (string.IsNullOrEmpty(aggregateId))
                throw BaseException.BadRequestException("AggregateId cannot be null or whitespace");

            var aggregate = new T();
            string streamName = $"{aggregate.GetType().Name}-{aggregateId}";
            long nextPageStart = 0;

            do
            {
                //eventleri version numarasına göre sıralıyoruz
                var events = await _eventStore.ReadStreamEventsForwardAsync(stream: streamName, start: nextPageStart, count: 4096, resolveLinkTos: false);
                if (events.Events.Length > 0)
                {
                    //Eventlerimİze aggragete uygulayarak aggragetin son halini oluşturuyoruz
                    aggregate.Load(
                        version: events.Events.Last().Event.EventNumber,
                        histories: events.Events.Select(c => 
                          JsonSerializer.Deserialize(Encoding.UTF8.GetString(c.OriginalEvent.Data), (typeof(T)))).ToArray());
                }

                nextPageStart = !events.IsEndOfStream ? events.NextEventNumber : -1;
            }
            while (nextPageStart != -1);

            return aggregate;
        }

        /// <summary>
        /// Event store'a event gönderir
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public async Task SaveAsync(T aggregate)
        {
            //ilgili event'leri alıyoruz, Event'ler eventstore'da EventData tipinde saklanır.
            var events = aggregate.GetChanges()
                .Select(c => new EventData(
                    eventId: Guid.NewGuid(),
                    type: c.GetType().Name,
                    isJson: true,
                    data: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(c)),
                    metadata: Encoding.UTF8.GetBytes(c.GetType().FullName)) // Eventleri deserialize ederken ilgili class'ı kullanmak için ilgili event'in class ismini verdik.
                ).ToArray();
            if (!events.Any())
                return;

            var streamName = $"{aggregate.GetType().Name}-{aggregate.Id}";
            await _eventStore.AppendToStreamAsync(stream: streamName, expectedVersion: ExpectedVersion.Any, events: events);
        }
    }
}
