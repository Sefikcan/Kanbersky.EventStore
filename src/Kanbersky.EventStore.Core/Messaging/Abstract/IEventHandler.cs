using Kanbersky.EventStore.Core.Messaging.Models;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Messaging.Abstract
{
    public interface IEventHandler<in TEvent> where TEvent : BaseMessagingModel
    {
        Task Handle(TEvent @event);
    }
}
