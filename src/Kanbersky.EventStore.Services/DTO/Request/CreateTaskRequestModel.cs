using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Services.DTO.Request
{
    public class CreateTaskRequestModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public int Version { get; private set; } = -1;

        public string Title { get; set; }

        public int Status { get; private set; } = (int)TaskStatus.Todo;

        public string AssignedBy { get; private set; } = string.Empty;

        public string CreatedBy { get; private set; } = "System";
    }
}
