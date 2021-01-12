using Kanbersky.EventStore.Core.Messaging.Models;
using System;

namespace Kanbersky.EventStore.Domain.EventModel
{
    public class AssignTaskEventModel : BaseMessagingModel
    {
        public Guid TaskId { get; set; }

        public string AssignedTo { get; set; }
    }
}
