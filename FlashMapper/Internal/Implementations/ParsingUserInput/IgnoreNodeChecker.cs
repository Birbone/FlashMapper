using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class IgnoreNodeChecker : IIgnoreNodeChecker
    {
        private readonly IEnumerable<ISpecificIgnoreNodeChecker> specificCheckers;

        public IgnoreNodeChecker(IEnumerable<ISpecificIgnoreNodeChecker> specificCheckers)
        {
            this.specificCheckers = specificCheckers;
        }

        public bool IsIgnoreNode(Expression node)
        {
            return specificCheckers.Any(c => c.IsIgnoreNode(node));
        }
    }
}