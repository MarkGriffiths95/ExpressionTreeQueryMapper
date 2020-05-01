using ExpressionTreeQueryMapper.Filters;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionTreeQueryMapper.ExtentionMethods
{
    public static class IEnumerableExtentionMethods
    {
        public static List<TSource> FindAllWhere<TSource>(this IEnumerable<TSource> source, IFilter<TSource> filter)
        {
            return source.AsQueryable().Where(filter.GenerateExpression()).ToList();
        }

        public static TSource FindSingleWhere<TSource>(this IEnumerable<TSource> source, IFilter<TSource> filter)
        {
            return source.AsQueryable().Where(filter.GenerateExpression()).SingleOrDefault();
        }

    }
}
