using Kanbersky.EventStore.Services.DTO.Request;

namespace Kanbersky.EventStore.Services.Abstract
{
    public interface ITaskContentAggregate
    {
        void Create(CreateTaskRequestModel createTaskRequest);
    }
}
