using FluentValidation;
using Kanbersky.EventStore.Core.EventStores.Repositories.Abstract;
using Kanbersky.EventStore.Core.Helpers;
using Kanbersky.EventStore.Services.Concrete;
using Kanbersky.EventStore.Services.DTO.Request;
using Kanbersky.EventStore.Services.DTO.Response;
using MediatR;
using System;
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

            RuleFor(c => c.CreateTaskRequestModel.Version)
                .GreaterThan(-1)
                .WithMessage("Task already created");
        }
    }

    public class CreateTaskRequestHandler : IRequestHandler<CreateTaskRequest, CreateTaskResponseModel>
    {
        private readonly IAggregateRepository<TaskContentAggregate> _aggregateRepository;

        public CreateTaskRequestHandler(IAggregateRepository<TaskContentAggregate> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task<CreateTaskResponseModel> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var taskAggregate = await _aggregateRepository.LoadAsync(Guid.NewGuid().ToString());
            taskAggregate.Create(request.CreateTaskRequestModel);
            await _aggregateRepository.SaveAsync(taskAggregate);

            return Mapping.Map<CreateTaskRequestModel, CreateTaskResponseModel>(request.CreateTaskRequestModel);
        }
    }
}
