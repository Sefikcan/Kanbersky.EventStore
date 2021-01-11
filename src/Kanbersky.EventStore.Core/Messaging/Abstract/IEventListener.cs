using MediatR;
using System;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Messaging.Abstract
{
    public interface IEventListener
    {
        void Subscribe(Type type);

        void Subscribe<TEvent>() where TEvent : INotification;

        Task Publish<TEvent>(TEvent @event) where TEvent : INotification;

        Task Publish(string message, string type);
    }
}
