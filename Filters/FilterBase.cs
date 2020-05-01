using System;
using System.Linq.Expressions;

namespace ExpressionTreeQueryMapper.Filters
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        protected static Expression<Func<T, bool>> _filter = model => true;
        protected static ExpressionType _expressionType;

        protected FilterBase()
        {
            _expressionType = ExpressionType.AndAlso;
        }

        protected FilterBase(ExpressionType expressionType)
        {
            _expressionType = expressionType;
        }


        public static Expression<Func<T, bool>> AddToFilter(Expression<Func<T, bool>> filter2)
        {
            var body = new ReplaceVisitor(_filter.Parameters[0], filter2.Parameters[0]).Visit(_filter.Body);
            
            var expr = Expression.Lambda<Func<T, bool>>(BuildExpressionByType(), filter2.Parameters);

            return expr;

            Expression BuildExpressionByType()
            {
                switch (_expressionType)
                {
                    case ExpressionType.AndAlso:
                        return Expression.AndAlso(body, filter2.Body);
                    case ExpressionType.OrElse:
                        return Expression.OrElse(body, filter2.Body);

                    default: throw new NotImplementedException($"This Expression Type has not been implemented");
                }
            }
        }


        public abstract Expression<Func<T, bool>> GenerateExpression();
    }
}
