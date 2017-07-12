using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class ConditionalIgnorePropertyAssignMapExpressionBuilderFactory : IConditionalIgnorePropertyAssignMapExpressionBuilderFactory
    {
        private readonly IIgnoreNodeChecker ignoreNodeChecker;
        public ConditionalIgnorePropertyAssignMapExpressionBuilderFactory(IIgnoreNodeChecker ignoreNodeChecker)
        {
            this.ignoreNodeChecker = ignoreNodeChecker;
        }

        public bool TryCreate(Expression leftOperand, Expression rightOperand, out IMapExpressionBuilder builder)
        {
            builder = null;
            var conditionalNode = rightOperand as ConditionalExpression;
            if (conditionalNode == null)
                return false;

            var ifTrueIsIgnore = ignoreNodeChecker.IsIgnoreNode(conditionalNode.IfTrue);
            var ifFalseIsIgnore = ignoreNodeChecker.IsIgnoreNode(conditionalNode.IfFalse);
            if (!ifTrueIsIgnore && !ifFalseIsIgnore)
                return false;
            if (ifTrueIsIgnore && ifFalseIsIgnore)
            {
                builder = new EmptyMapExpressionBuilder();
                return true;
            }
            if (ifFalseIsIgnore)
            {
                builder = new ConditionalAssignMapExpressionBuilder(conditionalNode.Test, leftOperand, conditionalNode.IfTrue);
                return true;
            }
            builder = new ConditionalAssignMapExpressionBuilder(Expression.IsFalse(conditionalNode.Test), leftOperand, conditionalNode.IfFalse);
            return true;
        }
    }
}