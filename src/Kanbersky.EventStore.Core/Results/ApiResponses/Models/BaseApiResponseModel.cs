namespace Kanbersky.EventStore.Core.Results.ApiResponses.Models
{
    public class BaseApiResponseModel<T>
    {
        public T Result { get; set; }

        public BaseApiResponseModel(T result)
        {
            Result = result;
        }
    }
}
