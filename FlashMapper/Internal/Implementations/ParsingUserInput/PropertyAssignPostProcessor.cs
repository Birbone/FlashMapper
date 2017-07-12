using System.Collections.Generic;
using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class PropertyAssignPostProcessor : IPropertyAssignPostProcessor
    {
        private readonly IEnumerable<ISpecificPropertyAssignMapExpressionBuilderFactory> specificExpressionBuilderFactories;

        public PropertyAssignPostProcessor(IEnumerable<ISpecificPropertyAssignMapExpressionBuilderFactory> specificExpressionBuilderFactories)
        {
            this.specificExpressionBuilderFactories = specificExpressionBuilderFactories;
        }

        public Expression Process(Expression inputExpression)
        {
            return new PropertyAssignPostProcessorVisitor(specificExpressionBuilderFactories).Visit(inputExpression);
        }
    }
}