using Microsoft.EntityFrameworkCore;

namespace MovieAnalytics.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }  // The current page number (e.g., 1, 2, 3).
        public int TotalPages { get; set; }   // Total number of pages based on data size and page size.
        public int PageSize { get; set; }     // Number of items per page (e.g., 10 items per page).
        public int TotalCount { get; set; }   // Total number of items in the data source.


        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            // ie page 1 -> 0 * 5. Skip 0 and Take 5 to get the first 5 resules
            // ie page 2 -> 1 * 5. Skip 5 and Take 5 to get the next 5 results
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
