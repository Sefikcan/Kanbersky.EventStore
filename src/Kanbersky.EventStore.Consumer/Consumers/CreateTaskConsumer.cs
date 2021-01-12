using Kanbersky.EventStore.Core.Messaging.Abstract;
using Kanbersky.EventStore.Domain.Concrete;
using Kanbersky.EventStore.Domain.EventModel;
using Kanbersky.EventStore.Infrastructure.Abstract;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Consumer.Consumers
{
    public class CreateTaskConsumer : IEventHandler<CreateTaskEventModel>
    {
        private readonly IGenericRepository<TaskContent> _taskContentRepository;

        public CreateTaskConsumer(IGenericRepository<TaskContent> taskContentRepository)
        {
            _taskContentRepository = taskContentRepository;
        }

        public async Task Handle(CreateTaskEventModel @event)
        {
            var doc = new TaskContent
            {
                Id = @event.TaskId.ToString(),
                AssignedTo = @event.AssignedBy,
                CreatedBy = @event.CreatedBy,
                Status = @event.Status,
                Title = @event.Title
            };

            await _taskContentRepository.AddAsync(doc);
        }
    }
}
