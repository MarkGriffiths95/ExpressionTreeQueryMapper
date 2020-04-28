using System.Linq.Expressions;

namespace ExpressionTreeQueryMapper.Filters
{
    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression _left, _right;

        public ReplaceVisitor(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override Expression Visit(Expression node)
        {
            return node == _left ? _right : base.Visit(node);
        }
    }
}
