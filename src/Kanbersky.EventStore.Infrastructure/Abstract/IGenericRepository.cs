using Kanbersky.EventStore.Core.Entity;
using Kanbersky.EventStore.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Infrastructure.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity, IEntity
    {
        Task<T> AddAsync(T entity);
        Task Remove(string key);
        Task<T> UpsertAsync(T entity);
        Task<T> FindAsync(string key);
        Task<PageableModel<T>> GetPageable(int pageSize, int page, string query);
        Task UpdateOneColumnAsync(string key, KeyValuePair<string, object> filter);
    }
}
