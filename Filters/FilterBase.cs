using System;
using System.Linq.Expressions;

namespace ExpressionTreeQueryMapper.Filters
{
    public abstract class FilterBase<T> : IFilter<T>
    {

        protected Expression<Func<T, bool>> filter = model => true;

        protected FilterBase()
        {

        }

        public static Expression<Func<T, bool>> Combine(Expression<Func<T, bool>> filter1, Expression<Func<T, bool>> filter2,ExpressionType expressionType = ExpressionType.AndAlso)
        {
            var body = new ReplaceVisitor(filter1.Parameters[0], filter2.Parameters[0]).Visit(filter1.Body);
            var expr = Expression.Lambda<Func<T, bool>>(BuildExpressionByType(), filter2.Parameters);

            return expr;

            Expression BuildExpressionByType()
            {
                switch (expressionType)
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
