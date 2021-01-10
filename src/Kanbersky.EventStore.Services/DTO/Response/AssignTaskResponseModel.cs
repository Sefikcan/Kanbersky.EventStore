using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Services.DTO.Response
{
    public class AssignTaskResponseModel
    {
        public Guid Id { get; set; }

        public long Version { get; set; }

        public TaskStatus Status { get; set; }

        public bool IsCompleted { get; set; }

        public string AssignedBy { get; set; }
    }
}
