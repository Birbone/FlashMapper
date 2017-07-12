using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface ISpecificUserInputParser
    {
        bool TryGetParts(Expression inputExpressionBody, out UserInputExpressionParts parts);
    }
}