namespace NewsAggregator.Services.Filters
{
    public class PaginationFilter
    {
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = (value > 20) ? 20 : value; }
        }
        private int _pageNumber = 1;
        public int PageNumber
        {
            get => _pageNumber;
            set { _pageNumber = (value >= 1) ? value : 1; }
        }

        public PaginationFilter()
        {
            PageSize = 5;
            PageNumber = 1;
        }
    }
}
