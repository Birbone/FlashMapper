using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class IsConstructionCheckPostProcessor : IIsConstructionCheckPostProcessor
    {
        private bool IsIsConstructionCall(Expression expression)
        {
            if (!(expression is MethodCallExpression methodCallExpression))
                return false;
            return methodCallExpression.Method.Name == nameof(MappingOptions.IsConstruction) &&
                   methodCallExpression.Method.DeclaringType == typeof(MappingOptions);
        }

        public Expression Process(Expression inputExpression, MappingPostProcessingContext context)
        {
            return inputExpression.ReplaceExpression(IsIsConstructionCall, Expression.Constant(context.IsConstruction));
        }
    }
}