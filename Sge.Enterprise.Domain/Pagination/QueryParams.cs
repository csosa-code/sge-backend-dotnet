namespace Sge.Enterprise.Domain.Pagination
{
    public class QueryParams
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public string? SortActive { get; set; }

        public string SortDirection { get; set; } = "asc";

        public string? Filter { get; set; }
    }
}