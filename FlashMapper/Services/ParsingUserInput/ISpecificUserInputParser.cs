using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface ISpecificUserInputParser
    {
        /// <summary>
        /// Tryes to get significant <see cref="parts"/> from <see cref="inputExpressionBody"/>
        /// </summary>
        /// <param name="inputExpressionBody"></param>
        /// <param name="parts"></param>
        /// <returns>True if this parser can get significant <see cref="parts"/> from <see cref="inputExpressionBody"/></returns>
        bool TryGetParts(Expression inputExpressionBody, out UserInputExpressionParts parts);
    }
}