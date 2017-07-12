using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class EmptyMapExpressionBuilder : IMapExpressionBuilder
    {
        public Expression GetExpression()
        {
            return Expression.Empty();
        }
    }
}