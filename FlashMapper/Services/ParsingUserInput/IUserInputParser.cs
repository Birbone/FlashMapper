using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface IUserInputParser : IFlashMapperService
    {
        UserInputExpressionParts GetUserInputParts(Expression inputExpressionBody);
    }
}