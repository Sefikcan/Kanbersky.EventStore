using FluentValidation;
using Kanbersky.EventStore.Core.EventStores.Repositories.Abstract;
using Kanbersky.EventStore.Core.Messaging.Abstract;
using Kanbersky.EventStore.Domain.EventModel;
using Kanbersky.EventStore.Services.Concrete;
using Kanbersky.EventStore.Services.DTO.Request;
using Kanbersky.EventStore.Services.DTO.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Services.Commands
{
    public class CreateTaskRequest : IRequest<CreateTaskResponseModel>
    {
        public CreateTaskRequestModel CreateTaskRequestModel { get; set; }

        public CreateTaskRequest(CreateTaskRequestModel createTaskRequestModel)
        {
            CreateTaskRequestModel = createTaskRequestModel;
        }
    }

    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(c => c.CreateTaskRequestModel.Title)
                .NotNull()
                .WithMessage("Title value cannot be null!");
        }
    }

    public class CreateTaskRequestHandler : IRequestHandler<CreateTaskRequest, CreateTaskResponseModel>
    {
        private readonly IAggregateRepository<TaskContentAggregate> _aggregateRepository;
        private readonly IEventListener _eventListener;

        public CreateTaskRequestHandler(IAggregateRepository<TaskContentAggregate> aggregateRepository,
            IEventListener eventListener)
        {
            _aggregateRepository = aggregateRepository;
            _eventListener = eventListener;
        }

        public async Task<CreateTaskResponseModel> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var taskAggregate = await _aggregateRepository.LoadAsync(request.CreateTaskRequestModel.Id.ToString());

            taskAggregate.Create(request.CreateTaskRequestModel);
            taskAggregate.Id = request.CreateTaskRequestModel.Id; //genel id bilgisi set edildi.

            await _aggregateRepository.SaveAsync(taskAggregate);

            return new CreateTaskResponseModel
            {
                AssignedBy = string.Empty,
                Version = request.CreateTaskRequestModel.Version,
                Id = request.CreateTaskRequestModel.Id,
                Status = (Core.Enums.TaskStatus)request.CreateTaskRequestModel.Status,
                Title = request.CreateTaskRequestModel.Title
            };
        }
    }
}
