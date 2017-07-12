using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class ExplicitIgnoreNodeChecker : IExplicitIgnoreNodeChecker
    {
        private readonly IImplicitIgnoreNodeChecker implicitIgnoreNodeChecker;
        public ExplicitIgnoreNodeChecker(IImplicitIgnoreNodeChecker implicitIgnoreNodeChecker)
        {
            this.implicitIgnoreNodeChecker = implicitIgnoreNodeChecker;
        }

        public bool IsIgnoreNode(Expression node)
        {
            while (node.NodeType == ExpressionType.Convert)
                node = ((UnaryExpression)node).Operand;
            if (node.NodeType != ExpressionType.Call)
                return false;
            var callNode = (MethodCallExpression)node;
            if (callNode.Method.DeclaringType == typeof(IgnoreObject) && 
                callNode.Method.Name == nameof(IgnoreObject.As))
            {
                return implicitIgnoreNodeChecker.IsIgnoreNode(callNode.Object);
            }
            return false;
        }
    }
}