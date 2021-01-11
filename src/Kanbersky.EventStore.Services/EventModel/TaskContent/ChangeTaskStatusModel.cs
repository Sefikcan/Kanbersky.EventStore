using System;

namespace Kanbersky.EventStore.Services.EventModel.TaskContent
{
    public class ChangeTaskStatusModel
    {
        public Guid Id { get; set; }

        public long Version { get; set; }

        public int Status { get; set; }

        public string UpdatedBy { get; set; } = "System";
    }
}
