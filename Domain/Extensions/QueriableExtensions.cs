using System.Linq.Expressions;
using MudBlazor;

namespace Domain.Extensions;

public static class QueryableExtensions {
    
    public static IQueryable<T> OrderByMultiple<T, TKey>(this IQueryable<T> query,
        (Expression<Func<T, TKey>> prop, SortDirection direction)[] sort) {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(sort);


        if (sort.Length == 0)
            return query;

        var ordered = query.OrderByDirection(sort[0].prop, sort[0].direction);

        if (sort.Length <= 1) return ordered;

        for (var i = 1; i < sort.Length; i++)
            ordered = ordered.ThenByDirection(sort[i].prop, sort[i].direction);

        return ordered;
    }

    public static IQueryable<T> SkipTake<T>(this IQueryable<T> query, int skip, int take) {
        ArgumentNullException.ThrowIfNull(query);

        return query.Skip(skip).Take(take);
    }

    public static IOrderedQueryable<TSource> OrderByDirection<TSource, TKey>(this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector, SortDirection direction) {
        return direction == SortDirection.Descending ?
            source.OrderByDescending(keySelector) :
            source.OrderBy(keySelector);
    }
    
    public static IOrderedQueryable<TSource> ThenByDirection<TSource, TKey>(this IOrderedQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector, SortDirection direction) {
        return direction == SortDirection.Descending ?
            source.ThenByDescending(keySelector) :
            source.ThenBy(keySelector);
    }
}