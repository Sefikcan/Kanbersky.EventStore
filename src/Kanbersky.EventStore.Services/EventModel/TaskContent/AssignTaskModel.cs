using System;

namespace Kanbersky.EventStore.Services.EventModel.TaskContent
{
    public class AssignTaskModel
    {
        public Guid Id { get; set; }

        public long Version { get; set; }

        public int Status { get; set; }

        public bool IsCompleted { get; set; }

        public string AssignedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
