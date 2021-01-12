using Kanbersky.EventStore.Core.Messaging.Models;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Messaging.Abstract
{
    public interface IEventListener
    {
        Task Publish<T>(T model) where T : BaseMessagingModel;

        void Subscribe<T, THandler>()
            where T : BaseMessagingModel
            where THandler : IEventHandler<T>;

        void UnSubscribe<T, THandler>()
            where T : BaseMessagingModel
            where THandler : IEventHandler<T>;
    }
}
