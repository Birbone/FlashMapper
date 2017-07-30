using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that parses user defined expression on significant parts
    /// </summary>
    public interface IUserInputParser : IFlashMapperService
    {
        /// <summary>
        /// Parses input expressions and returns significant parts from it
        /// </summary>
        /// <param name="inputExpressionBody"></param>
        /// <returns></returns>
        UserInputExpressionParts GetUserInputParts(Expression inputExpressionBody);
    }
}