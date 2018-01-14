using System;
using System.Linq.Expressions;

namespace FlashMapper.Internal
{
    internal static class ProcessExpressionHelpers
    {
        public static Expression ReplaceExpression(this Expression inputExpression, 
            Func<Expression, bool> expressionPredicate, Expression expression)
        {
            return inputExpression.ReplaceExpression(expressionPredicate, i => expression);
        }

        public static Expression ReplaceExpression(this Expression inputExpression,
            Func<Expression, bool> expressionPredicate, Func<Expression, Expression> replaceFunc)
        {
            return new ReplaceExpressionVisitor(expressionPredicate, replaceFunc).Visit(inputExpression);
        }
    }
}