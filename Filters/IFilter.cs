using System;
using System.Linq.Expressions;

namespace ExpressionTreeQueryMapper.Filters
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> GenerateExpression();

        
    }
}
