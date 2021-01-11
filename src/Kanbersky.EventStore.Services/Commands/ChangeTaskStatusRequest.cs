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
    public class ChangeTaskStatusRequest : IRequest<ChangeTaskStatusResponseModel>
    {
        public Guid Id { get; set; }

        public ChangeTaskStatusRequestModel ChangeTaskStatusRequestModel { get; set; }

        public ChangeTaskStatusRequest(Guid id, 
            ChangeTaskStatusRequestModel changeTaskStatusRequestModel)
        {
            Id = id;
            ChangeTaskStatusRequestModel = changeTaskStatusRequestModel;
        }
    }

    public class ChangeTaskStatusRequestValidator : AbstractValidator<ChangeTaskStatusRequest>
    {
        public ChangeTaskStatusRequestValidator()
        {
            RuleFor(c => c.ChangeTaskStatusRequestModel.Status)
                .NotNull()
                .WithMessage("Status value cannot be null!");

            RuleFor(c => c.ChangeTaskStatusRequestModel.Status).SetValidator(new TaskStatusValidator<Core.Enums.TaskStatus>());
        }
    }


    public class ChangeTaskStatusRequestHandler : IRequestHandler<ChangeTaskStatusRequest, ChangeTaskStatusResponseModel>
    {
        private readonly IAggregateRepository<TaskContentAggregate> _aggregateRepository;

        public ChangeTaskStatusRequestHandler(IAggregateRepository<TaskContentAggregate> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task<ChangeTaskStatusResponseModel> Handle(ChangeTaskStatusRequest request, CancellationToken cancellationToken)
        {
            var taskAggregate = await _aggregateRepository.LoadAsync(request.Id.ToString());
            taskAggregate.ChangeStatus(request.Id, request.ChangeTaskStatusRequestModel);
            taskAggregate.Id = request.Id; //genel id bilgisi set edildi.
            await _aggregateRepository.SaveAsync(taskAggregate);

            return new ChangeTaskStatusResponseModel
            {
                UpdatedBy = request.ChangeTaskStatusRequestModel.UpdatedBy,
                Id = request.Id,
                Status = request.ChangeTaskStatusRequestModel.Status,
                Version = taskAggregate.Version
            };
        }
    }
}
