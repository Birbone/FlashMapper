using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class IgnorePropertyAssignMapExpressionBuilderFactory : IIgnorePropertyAssignMapExpressionBuilderFactory
    {
        private readonly IIgnoreNodeChecker ignoreNodeChecker;
        public IgnorePropertyAssignMapExpressionBuilderFactory(IIgnoreNodeChecker ignoreNodeChecker)
        {
            this.ignoreNodeChecker = ignoreNodeChecker;
        }

        public bool TryCreate(Expression leftOperand, Expression rightOperand, out IMapExpressionBuilder builder)
        {
            if (ignoreNodeChecker.IsIgnoreNode(rightOperand))
            {
                builder = new EmptyMapExpressionBuilder();
                return true;
            }
            builder = null;
            return false;
        }
    }
}