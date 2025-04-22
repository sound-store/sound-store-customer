namespace SoundStore.Commons
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = [];
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
