using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class ProcessIgnoreVisitor : ExpressionVisitor
    {
        private bool IsMappingOptionsIgnoreMethod(MethodInfo method)
        {
            return method.DeclaringType == typeof(MappingOptions) &&
                   method.Name == nameof(MappingOptions.Ignore);
        }

        private bool IsMappingOptionsIgnoreAsMethod(MethodInfo method)
        {
            return method.DeclaringType == typeof(IgnoreObject) &&
                   method.Name == nameof(IgnoreObject.As);
        }

        private bool IsIgnoreNode(Expression node)
        {

            while (node.NodeType == ExpressionType.Convert)
                node = ((UnaryExpression) node).Operand;
            if (node.NodeType != ExpressionType.Call)
                return false;
            var callNode = (MethodCallExpression) node;
            return callNode.Method.DeclaringType == typeof(MappingOptions) &&
                   callNode.Method.Name == nameof(MappingOptions.Ignore);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Assign)
                return base.VisitBinary(node);
            if (IsIgnoreNode(node.Right))
                return Expression.Empty();
            var conditional = node.Right as ConditionalExpression;
            if (conditional == null)
                return base.VisitBinary(node);
            var ifTrueIsIgnore = IsIgnoreNode(conditional.IfTrue);
            var ifFalseIsIgnore = IsIgnoreNode(conditional.IfFalse);
            if (ifTrueIsIgnore && ifFalseIsIgnore)
                return Expression.Empty();
            if (!ifTrueIsIgnore && !ifFalseIsIgnore)
                return base.VisitBinary(node);

            if (ifFalseIsIgnore)
            {
                var assignTrue = Expression.Assign(node.Left, conditional.IfTrue);
                return Expression.IfThen(conditional.Test, assignTrue);
            }

            var assignFalse = Expression.Assign(node.Left, conditional.IfFalse);
            var invertedCondition = Expression.IsFalse(conditional.Test);
            return Expression.IfThen(invertedCondition, assignFalse);
        }
    }
}