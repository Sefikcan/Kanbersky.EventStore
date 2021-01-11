using Kanbersky.EventStore.Services.DTO.Request;
using System;

namespace Kanbersky.EventStore.Services.Abstract
{
    public interface ITaskContentAggregate
    {
        void Create(CreateTaskRequestModel createTaskRequest);

        void Assign(Guid id, AssignTaskRequestModel assignTaskRequestModel);

        void ChangeStatus(Guid id, ChangeTaskStatusRequestModel changeTaskStatusRequest);
    }
}
