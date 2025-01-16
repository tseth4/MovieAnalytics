namespace MovieAnalytics.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        // A public property with getter and setter:
        public int PageSize
        {
            get => _pageSize;
            //  Enforces the maximum page size
            // If the value provided by the client exceeds MaxPageSize, it defaults to MaxPageSize.
            // Otherwise, it accepts the client-provided value.
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
