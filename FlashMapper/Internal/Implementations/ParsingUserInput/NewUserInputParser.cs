using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class NewUserInputParser : INewUserInputParser
    {
        public bool TryGetParts(Expression inputExpressionBody, out UserInputExpressionParts parts)
        {
            var newExpression = inputExpressionBody as NewExpression;
            if (newExpression == null || newExpression.NodeType != ExpressionType.New)
            {
                parts = null;
                return false;
            }
            parts = new UserInputExpressionParts
            {
                Bindings = new MemberBinding[0],
                ModelCreateExpression = newExpression
            };
            return true;
        }
    }
}