using Couchbase;
using Kanbersky.EventStore.Core.Entity;

namespace Kanbersky.EventStore.Core.Extensions
{
    public static class EntityExtensions
    {
        public static IDocument<T> ConvertDocument<T>(this T entity) where T : BaseEntity, IEntity
        {
            return new Document<T>
            {
                Id = entity.Id,
                Cas = entity.Cas,
                Content = entity
            };
        }

        public static T ConvertEntity<T>(this IDocument<T> document) where T : BaseEntity, IEntity
        {
            var entity = document.Content;
            if (entity != null)
            {
                entity.Id = document.Id;
                entity.Cas = document.Cas;
            }

            return entity;
        }
    }
}
