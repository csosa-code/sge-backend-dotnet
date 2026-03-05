namespace Sge.Enterprise.Domain.Pagination
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}