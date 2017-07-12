using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class ImplicitIgnoreNodeChecker : IImplicitIgnoreNodeChecker
    {
        public bool IsIgnoreNode(Expression node)
        {
            while (node.NodeType == ExpressionType.Convert)
                node = ((UnaryExpression)node).Operand;
            if (node.NodeType != ExpressionType.Call)
                return false;
            var callNode = (MethodCallExpression)node;
            return callNode.Method.DeclaringType == typeof(MappingOptions) &&
                   callNode.Method.Name == nameof(MappingOptions.Ignore);
        }
    }
}