using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class UserInputParser : IUserInputParser
    {
        private readonly IEnumerable<ISpecificUserInputParser> specificParsers;

        public UserInputParser(IEnumerable<ISpecificUserInputParser> specificParsers)
        {
            this.specificParsers = specificParsers.ToArray();
        }

        public UserInputExpressionParts GetUserInputParts(Expression inputExpressionBody)
        {
            UserInputExpressionParts result = null;

            if (specificParsers.Any(p => p.TryGetParts(inputExpressionBody, out result)))
            {
                return result;
            }

            throw new FlashMapperBadInputException($"Mapping expression has incorrect format. Expression of type {inputExpressionBody?.NodeType} is not supported.");
        }
    }
}