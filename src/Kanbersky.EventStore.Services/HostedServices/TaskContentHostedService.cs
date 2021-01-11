using EventStore.ClientAPI;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using Kanbersky.EventStore.Domain.Concrete;
using Kanbersky.EventStore.Infrastructure.Abstract;
using Kanbersky.EventStore.Services.EventModel.TaskContent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Services.HostedServices
{
    public class TaskContentHostedService : IHostedService
    {
        private readonly IGenericRepository<TaskContent> _taskContentRepository;
        private readonly IGenericRepository<TaskPosition> _taskPositionRepository;
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<TaskContentHostedService> _logger;
        private EventStoreAllCatchUpSubscription _subscription;

        public TaskContentHostedService(IGenericRepository<TaskContent> taskContentRepository,
            IGenericRepository<TaskPosition> taskPositionRepository,
            IEventStoreConnection eventStoreConnection,
            ILogger<TaskContentHostedService> logger)
        {
            _taskContentRepository = taskContentRepository;
            _taskPositionRepository = taskPositionRepository;
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //eventler arasından en son kayıt getirilir.
            var lastTaskContentPosition = await _taskPositionRepository.FindAsync("TaskContent");

            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "TaskContent");

            //ilgili event store'a subscribe olup eventler dinlenir, verilen son position değeri ile tekrar tekrar proje ayağa kalktığında eventleri gezmesini engelledik.
            _subscription = _eventStoreConnection.SubscribeToAllFrom(
                lastCheckpoint: lastTaskContentPosition?.Position,
                settings: settings,
                eventAppeared: async (sub, @event) =>
                {
                    //Event Store’un kendine ait event‘leri ile ilgili işlem yapmamak için pas geçiyoruz.
                    if (@event.OriginalEvent.EventType.StartsWith("$"))
                        return;

                    try
                    {
                        var eventType = Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata));
                        var eventData = JsonSerializer.Deserialize(Encoding.UTF8.GetString(@event.OriginalEvent.Data), eventType);

                        if (eventType != typeof(AssignTaskModel) && eventType != typeof(ChangeTaskStatusModel) && eventType != typeof(CreateTaskModel))
                            return;

                        Save(eventData);

                        var doc = new TaskPosition
                        {
                            Id = "TaskContent",
                            Key = "TaskContent",
                            Position = @event.OriginalPosition.GetValueOrDefault()
                        };

                        await _taskPositionRepository.UpsertAsync(doc);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, exception.Message);
                    }
                });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscription.Stop();
            return Task.CompletedTask;
        }

        private void Save(object @event)
        {
            switch (@event)
            {
                case CreateTaskModel x: OnCreated(x); break;
                case AssignTaskModel x: OnAssigned(x); break;
                case ChangeTaskStatusModel x: OnChanged(x); break;
                default:
                    throw BaseException.BadRequestException("Unsupported event type");
            }
        }

        private async void OnCreated(CreateTaskModel @event)
        {
            var doc = new TaskContent
            {
                Id = @event.Id.ToString(),
                AssignedTo = @event.AssignedBy,
                CreatedBy = @event.CreatedBy,
                Status = @event.Status,
                Title = @event.Title
            };

            await _taskContentRepository.AddAsync(doc);
        }

        private async void OnAssigned(AssignTaskModel @event)
        {
            await _taskContentRepository.UpdateOneColumnAsync(@event.Id.ToString(), "assignedTo" , @event.AssignedTo);
        }

        private async void OnChanged(ChangeTaskStatusModel @event)
        {
            await _taskContentRepository.UpdateOneColumnAsync(@event.Id.ToString(), "status" , @event.Status);
        }
    }
}
