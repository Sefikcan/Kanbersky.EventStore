using System.Collections.Generic;

namespace Kanbersky.EventStore.Core.Models
{
    public class PageableModel<T>
    {
        public IList<T> Items { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPageCount { get; set; }

        public int TotalItemCount { get; set; }
    }
}
