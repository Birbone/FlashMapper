using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface IMapExpressionBuilder
    {
        Expression GetExpression();
    }
}