using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Services.DTO.Response
{
    public class TaskResponseModel
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public string AssignedTo { get; set; }

        public string UpdatedBy { get; set; }

        public TaskStatus Status { get; set; }

        public string CreatedBy { get; set; }
    }
}
