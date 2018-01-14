using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FlashMapper.DependencyInjection
{
    internal class ToStaticMethodVisitor:ExpressionVisitor
    {
        private readonly object instance;
        private readonly ParameterExpression replacementParameter;

        public ToStaticMethodVisitor(object instance, ParameterExpression replacementParameter)
        {
            this.instance = instance;
            this.replacementParameter = replacementParameter;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return node.Value == instance 
                ? replacementParameter 
                : base.VisitConstant(node);
        }
    }

    internal class ExpressionReplacement
    {
        public ExpressionReplacement(Expression sourceExpression, Expression replacementExpression)
        {
            SourceExpression = sourceExpression;
            ReplacementExpression = replacementExpression;
        }

        public Expression SourceExpression { get; }
        public Expression ReplacementExpression { get; }
    }

    internal class ExpressionReplacementVisitor : ExpressionVisitor
    {
        private readonly List<ExpressionReplacement> replacements;
        
        public ExpressionReplacementVisitor(IEnumerable<ExpressionReplacement> replacements)
        {
            this.replacements = replacements.ToList();
        }

        public override Expression Visit(Expression node)
        {
            var suiteableReplacement = replacements.FirstOrDefault(r => r.SourceExpression == node);
            if (suiteableReplacement == null)
                return base.Visit(node);
            return suiteableReplacement.ReplacementExpression;
        }
    }
}