using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Kanbersky.EventStore.Core.Entity;
using Kanbersky.EventStore.Core.Extensions;
using Kanbersky.EventStore.Core.Models;
using Kanbersky.EventStore.Core.Results.Exceptions.Concrete;
using Kanbersky.EventStore.Infrastructure.Abstract;
using System;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Infrastructure.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IEntity
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly IBucket _bucket;

        public GenericRepository(IBucketProvider bucketProvider)
        {
            _bucketProvider = bucketProvider;
            _bucket = _bucketProvider.GetBucket(typeof(T).Name);
        }

        public async Task<T> AddAsync(T entity)
        {
            var results = await _bucket.InsertAsync(entity.ConvertDocument());
            return results.Document.ConvertEntity();
        }

        public async Task<T> FindAsync(string key)
        {
            var result = await _bucket.GetDocumentAsync<T>(key);
            return result.Document.ConvertEntity();
        }

        public async Task<PageableModel<T>> GetPageable(int pageSize, int page, string query)
        {
            var results = await _bucket.QueryAsync<T>(query);
            if (results.Success)
            {
                return new PageableModel<T>
                {
                    Items = results.Rows,
                    PageNumber = page,
                    PageSize = results.Rows.Count,
                    TotalItemCount = Convert.ToInt32(results.Metrics.SortCount),
                    TotalPageCount = (int)Math.Ceiling(Convert.ToDouble(results.Metrics.SortCount) / pageSize)
                };
            }

            throw BaseException.BadRequestException(results.Message);
        }

        public async Task Remove(string key)
        {
            await _bucket.RemoveAsync(key);
        }

        public async Task<T> UpsertAsync(T entity)
        {
            var result = await _bucket.UpsertAsync(entity.ConvertDocument());
            return result.Document.ConvertEntity();
        }
    }
}
