using Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using Kanbersky.EventStore.Services.Abstract;
using Kanbersky.EventStore.Services.DTO.Request;
using Kanbersky.EventStore.Services.EventModel.TaskContent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kanbersky.EventStore.Services.Concrete
{
    public class TaskContentAggregate : BaseAggregate, IAggregate, ITaskContentAggregate
    {
        //İlgili aggragete'e ait event'leri store edeceğimiz değişken
        private readonly IList<object> _changes = new List<object>();

        public Guid Id { get; set; } = Guid.Empty;

        public long Version { get; set; } = -1;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int Status { get; set; }

        public string AssignedBy { get; set; }

        public string UpdatedBy { get; set; }

        public void Apply(object @event)
        {
            When(@event);
            _changes.Add(@event);
        }

        public void Create(CreateTaskRequestModel createTaskRequest)
        {
            if (createTaskRequest.Version >= 0)
                throw BaseException.BadRequestException("Task already created");

            Apply(new CreateTaskModel 
            {
                AssignedBy = createTaskRequest.AssignedBy,
                Id = createTaskRequest.Id,
                Status = createTaskRequest.Status,
                Title = createTaskRequest.Title,
                Version = createTaskRequest.Version
            });
        }

        public void Assign(Guid id, AssignTaskRequestModel assignTaskRequestModel)
        {
            if (Version == -1)
                throw BaseException.NotFoundException("Task Not Found!");

            Apply(new AssignTaskModel 
            {
                AssignedBy = assignTaskRequestModel.AssignedBy,
                Id = id,
                UpdatedBy = assignTaskRequestModel.UpdatedBy,
                Status = assignTaskRequestModel.Status,
                Version = Version
            });
        }

        public void ChangeStatus(Guid id, ChangeTaskStatusRequestModel changeTaskStatusRequest)
        {
            if (Version == -1)
                throw BaseException.NotFoundException("Task Not Found!");

            Apply(new ChangeTaskStatusModel
            {
                Id = id,
                UpdatedBy = changeTaskStatusRequest.UpdatedBy,
                Status = (int)changeTaskStatusRequest.Status,
                Version = Version
            });
        }

        public object[] GetChanges()
        {
            return _changes.ToArray();
        }

        public void Load(long version, IEnumerable<object> histories)
        {
            Version = version;
            foreach (var history in histories)
            {
                When(history);
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case CreateTaskModel x: OnCreated(x);
                    break;
                case AssignTaskModel x: OnAssigned(x);
                    break;
                default:
                    break;
            }
        }

        private void OnCreated(CreateTaskModel createTaskRequest)
        {
            Id = createTaskRequest.Id;
            Version = createTaskRequest.Version;
            CreatedDate = DateTime.Now;
            Status = createTaskRequest.Status;
            AssignedBy = createTaskRequest.AssignedBy;
            UpdatedBy = createTaskRequest.UpdatedBy;
        }

        private void OnAssigned(AssignTaskModel assignTask)
        {
            Id = assignTask.Id;
            Version = assignTask.Version;
            CreatedDate = DateTime.Now;
            Status = assignTask.Status;
            AssignedBy = assignTask.AssignedBy;
            UpdatedBy = assignTask.UpdatedBy;
        }
    }
}
