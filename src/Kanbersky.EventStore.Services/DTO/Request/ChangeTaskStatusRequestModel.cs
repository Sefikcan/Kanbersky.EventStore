namespace Kanbersky.EventStore.Services.DTO.Request
{
    public class ChangeTaskStatusRequestModel
    {
        public Core.Enums.TaskStatus Status { get; set; }
        public string UpdatedBy { get; private set; } = "Systemabc";
    }
}
