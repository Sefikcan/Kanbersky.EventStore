using Kanbersky.EventStore.Core.EventStores.Aggregate.Abstract;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using Kanbersky.EventStore.Services.Abstract;
using Kanbersky.EventStore.Services.DTO.Request;
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

        public void Apply(object @event)
        {
            When(@event);
            _changes.Add(@event);
        }

        public void Create(CreateTaskRequestModel createTaskRequest)
        {
            if (createTaskRequest.Version >= 0)
                throw BaseException.BadRequestException("Task already created");

            Apply(createTaskRequest);
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
                case CreateTaskRequestModel x: OnCreated(x);
                    break;

                default:
                    break;
            }
        }

        private void OnCreated(CreateTaskRequestModel createTaskRequest)
        {

        }
    }
}
