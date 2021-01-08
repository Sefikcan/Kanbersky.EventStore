using Kanbersky.EventStore.Core.Results.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using System;

namespace Kanbersky.EventStore.Core.Results.Exceptions.Concrete
{
    public class NotImplementedException : Exception, IBaseException
    {
        public int BaseStatusCode { get; set; }

        public NotImplementedException()
        {
            BaseStatusCode = StatusCodes.Status501NotImplemented;
        }

        public NotImplementedException(string message) : base(message)
        {
            BaseStatusCode = StatusCodes.Status501NotImplemented;
        }

        public NotImplementedException(string message, Exception exception) : base(message, exception)
        {
            BaseStatusCode = StatusCodes.Status501NotImplemented;
        }
    }
}
