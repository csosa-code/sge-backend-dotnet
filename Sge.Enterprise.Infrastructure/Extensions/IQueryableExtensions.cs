using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Sge.Enterprise.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> query,
            string? sortBy,
            string? sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            var property = typeof(T).GetProperty(
                sortBy,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (property == null)
                return query;

            query = sortDirection?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true
                ? query.OrderByDescending(e => EF.Property<object>(e!, property.Name))
                : query.OrderBy(e => EF.Property<object>(e!, property.Name));

            return query;
        }

        public static IQueryable<T> ApplyPagination<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return query;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}