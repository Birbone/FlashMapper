using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface IMapExpressionPostProcessor: IFlashMapperService
    {
        Expression Process(Expression inputExpression);
    }
}