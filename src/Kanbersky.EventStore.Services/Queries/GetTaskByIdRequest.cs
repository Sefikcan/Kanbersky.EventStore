using Kanbersky.EventStore.Core.Helpers;
using Kanbersky.EventStore.Domain.Concrete;
using Kanbersky.EventStore.Infrastructure.Abstract;
using Kanbersky.EventStore.Services.DTO.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Services.Queries
{
    public class GetTaskByIdRequest : IRequest<TaskResponseModel>
    {
        public Guid Id { get; set; }

        public GetTaskByIdRequest(Guid id)
        {
            Id = id;
        }
    }

    public class GetTaskByIdRequestHandler : IRequestHandler<GetTaskByIdRequest, TaskResponseModel>
    {
        private readonly IGenericRepository<TaskContent> _taskRepository;

        public GetTaskByIdRequestHandler(IGenericRepository<TaskContent> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskResponseModel> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.FindAsync(request.Id.ToString());
            return Mapping.Map<TaskContent, TaskResponseModel>(task);
        }
    }
}
