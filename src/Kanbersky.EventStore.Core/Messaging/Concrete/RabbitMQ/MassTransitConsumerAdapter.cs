using Kanbersky.EventStore.Core.Messaging.Abstract;
using Kanbersky.EventStore.Core.Messaging.Models;
using MassTransit;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Messaging.Concrete.RabbitMQ
{
    public class MassTransitConsumerAdapter<T> : IConsumer<T> where T : BaseMessagingModel
    {
        private readonly IEventHandler<T> _eventHandler;

        public MassTransitConsumerAdapter(IEventHandler<T> eventHandler)
        {
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            await _eventHandler.Handle(context.Message);
        }
    }
}
