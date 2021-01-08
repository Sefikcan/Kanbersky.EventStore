using AutoMapper;

namespace Kanbersky.EventStore.Core.Helpers
{
    public static class Mapping
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = new Mapper(config);

            return mapper.Map<TSource, TTarget>(source);
        }
    }
}
