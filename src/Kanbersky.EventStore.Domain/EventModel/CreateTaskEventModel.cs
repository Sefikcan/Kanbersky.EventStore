﻿using Kanbersky.EventStore.Core.Messaging.Models;
using System;

namespace Kanbersky.EventStore.Domain.EventModel
{
    public class CreateTaskEventModel : BaseMessagingModel
    {
        public Guid TaskId { get; set; }

        public int Version { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }

        public string AssignedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string CreatedBy { get; set; }
    }
}
