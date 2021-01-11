using Kanbersky.EventStore.Core.Enums;

namespace Kanbersky.EventStore.Services.DTO.Request
{
    public class AssignTaskRequestModel
    {
        public string AssignedBy { get; set; }

        public string UpdatedBy { get; private set; } = "System";

        public int Status { get; private set; } = (int)TaskStatus.Todo;
    }
}
