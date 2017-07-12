using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class IgnoreMapExpressionPostProcessor : IIgnoreMapExpressionPostProcessor
    {
        public Expression Process(Expression inputExpression)
        {
            return new ProcessIgnoreVisitor().Visit(inputExpression);
        }
    }
}