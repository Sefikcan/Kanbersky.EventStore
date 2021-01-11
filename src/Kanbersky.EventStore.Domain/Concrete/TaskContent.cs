using Kanbersky.EventStore.Core.Entity;

namespace Kanbersky.EventStore.Domain.Concrete
{
    public class TaskContent : BaseEntity, IEntity
    {
        public string Title { get; set; }

        public string CreatedBy { get; set; } = "System";

        public string AssignedTo { get; set; }

        public int Status { get; set; }
    }
}
