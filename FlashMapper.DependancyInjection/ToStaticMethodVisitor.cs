using System.Linq.Expressions;

namespace FlashMapper.DependancyInjection
{
    public class ToStaticMethodVisitor:ExpressionVisitor
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
}