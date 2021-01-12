using System;

namespace Kanbersky.EventStore.Core.Messaging.Models
{
    public class BaseMessagingModel
    {
        public Guid QueueId { get; set; }

        public DateTime CreatedDate { get; set; }

        public BaseMessagingModel()
        {
            QueueId = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public BaseMessagingModel(Guid queuId, DateTime createdDate)
        {
            QueueId = queuId;
            CreatedDate = createdDate;
        }
    }
}
