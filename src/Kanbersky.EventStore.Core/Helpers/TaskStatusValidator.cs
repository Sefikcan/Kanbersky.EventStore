using FluentValidation.Validators;
using Kanbersky.EventStore.Core.Enums;
using System;

namespace Kanbersky.EventStore.Core.Helpers
{
    public class TaskStatusValidator<T> : PropertyValidator
    {
        public TaskStatusValidator() : base("Invalid enum value!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            TaskStatus enumVal = (TaskStatus)Enum.Parse(typeof(TaskStatus), context.PropertyValue.ToString());

            if (!Enum.IsDefined(typeof(TaskStatus), enumVal))
              return false;

            return true;
        }
    }
}
