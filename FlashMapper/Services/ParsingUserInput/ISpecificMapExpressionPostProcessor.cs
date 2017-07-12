using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface ISpecificMapExpressionPostProcessor
    {
        Expression Process(Expression inputExpression);
    }
}