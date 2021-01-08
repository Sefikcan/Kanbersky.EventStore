namespace Kanbersky.EventStore.Core.Results.Exceptions.Abstract
{
    public interface IBaseException
    {
        public int BaseStatusCode { get; set; }
    }
}
