using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class MapExpressionPostProcessor : IMapExpressionPostProcessor
    {
        private readonly IEnumerable<ISpecificMapExpressionPostProcessor> allProcessors;
        public MapExpressionPostProcessor(IEnumerable<ISpecificMapExpressionPostProcessor> allProcessors)
        {
            this.allProcessors = allProcessors;
        }

        public Expression Process(Expression inputExpression)
        {
            return allProcessors.Aggregate(inputExpression, (e, p) => p.Process(e));
        }
    }
}