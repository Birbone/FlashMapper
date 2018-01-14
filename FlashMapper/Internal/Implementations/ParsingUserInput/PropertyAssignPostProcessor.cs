using System.Collections.Generic;
using System.Linq.Expressions;
using FlashMapper.Models;
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

        public Expression Process(Expression inputExpression, MappingPostProcessingContext context)
        {
            return new PropertyAssignPostProcessorVisitor(specificExpressionBuilderFactories).Visit(inputExpression);
        }
    }
}