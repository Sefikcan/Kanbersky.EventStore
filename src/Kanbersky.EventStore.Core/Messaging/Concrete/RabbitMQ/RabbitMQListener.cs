using Kanbersky.EventStore.Core.Messaging.Abstract;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Messaging.Concrete.RabbitMQ
{
    public class RabbitMQListener : IEventListener
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : INotification
        {
            throw new NotImplementedException();
        }

        public Task Publish(string message, string type)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Type type)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TEvent>() where TEvent : INotification
        {
            throw new NotImplementedException();
        }
    }
}
