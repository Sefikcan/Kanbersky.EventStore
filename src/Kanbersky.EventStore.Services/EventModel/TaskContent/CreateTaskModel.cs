using System;

namespace Kanbersky.EventStore.Services.EventModel.TaskContent
{
    public class CreateTaskModel
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }

        public string AssignedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedBy { get; private set; } = "System";
    }
}
