using System;
using System.Linq.Expressions;

namespace FlashMapper.Internal
{
    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Func<Expression, bool> expressionPredicate;
        private readonly Func<Expression, Expression> replaceFunc;

        public ReplaceExpressionVisitor(Func<Expression, bool> expressionPredicate, Func<Expression, Expression> replaceFunc)
        {
            this.expressionPredicate = expressionPredicate;
            this.replaceFunc = replaceFunc;
        }

        public override Expression Visit(Expression node)
        {
            return expressionPredicate(node) 
                ? replaceFunc(node) 
                : base.Visit(node);
        }
    }
}