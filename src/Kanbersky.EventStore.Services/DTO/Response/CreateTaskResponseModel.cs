using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Services.DTO.Response
{
    public class CreateTaskResponseModel
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public string Title { get; set; }

        public TaskStatus Status { get; set; }

        public string AssignedBy { get; set; }
    }
}
