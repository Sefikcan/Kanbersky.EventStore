using Kanbersky.EventStore.Core.Messaging.Models;
using System;

namespace Kanbersky.EventStore.Domain.EventModel
{
    public class ChangeTaskStatusEventModel : BaseMessagingModel
    {
        public Guid TaskId { get; set; }

        public int Status { get; set; }
    }
}
