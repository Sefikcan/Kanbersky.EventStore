using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Services.DTO.Response
{
    public class ChangeTaskStatusResponseModel
    {
        public string UpdatedBy { get; set; }

        public Guid Id { get; set; }

        public long Version { get; set; }

        public TaskStatus Status { get; set; }
    }
}
