using Kanbersky.EventStore.Core.Entity;
using Kanbersky.EventStore.Core.Enums;

namespace Kanbersky.EventStore.Domain.Concrete
{
    public class TaskContent : BaseEntity, IEntity
    {
        public string Title { get; set; }

        public string CreatedBy { get; set; } = "System";

        public string AssignedTo { get; set; }

        public TaskStatus Status { get; set; }
    }
}
