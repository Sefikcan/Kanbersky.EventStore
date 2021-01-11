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
    public class AssignTaskRequest : IRequest<AssignTaskResponseModel>
    {
        public Guid Id { get; set; }

        public AssignTaskRequestModel AssignTaskRequestModel { get; set; }

        public AssignTaskRequest(Guid id,
            AssignTaskRequestModel assignTaskRequestModel)
        {
            Id = id;
            AssignTaskRequestModel = assignTaskRequestModel;
        }
    }

    public class AssignTaskRequestValidator : AbstractValidator<AssignTaskRequest>
    {
        public AssignTaskRequestValidator()
        {
            RuleFor(c => c.AssignTaskRequestModel.AssignedTo)
                .NotEmpty()
                .WithMessage("AssignedBy value cannot be empty!")
                .NotNull()
                .WithMessage("AssignedBy value cannot be null!");
        }
    }

    public class AssignTaskRequestHandler : IRequestHandler<AssignTaskRequest, AssignTaskResponseModel>
    {
        private readonly IAggregateRepository<TaskContentAggregate> _aggregateRepository;

        public AssignTaskRequestHandler(IAggregateRepository<TaskContentAggregate> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task<AssignTaskResponseModel> Handle(AssignTaskRequest request, CancellationToken cancellationToken)
        {
            var taskAggregate = await _aggregateRepository.LoadAsync(request.Id.ToString());
            taskAggregate.Assign(request.Id ,request.AssignTaskRequestModel);
            taskAggregate.Id = request.Id; //genel id bilgisi set edildi.
            await _aggregateRepository.SaveAsync(taskAggregate);

            return new AssignTaskResponseModel
            {
                AssignedBy = request.AssignTaskRequestModel.AssignedTo,
                Id = request.Id,
                Status = (Core.Enums.TaskStatus)request.AssignTaskRequestModel.Status,
                Version = taskAggregate.Version
            };
        }
    }
}
