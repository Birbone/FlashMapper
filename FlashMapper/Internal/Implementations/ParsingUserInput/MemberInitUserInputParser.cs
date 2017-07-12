using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class MemberInitUserInputParser : IMemberInitUserInputParser
    {
        public bool TryGetParts(Expression inputExpressionBody, out UserInputExpressionParts parts)
        {
            var memberInitExpression = inputExpressionBody as MemberInitExpression;
            if (memberInitExpression == null || memberInitExpression.NodeType != ExpressionType.MemberInit)
            {
                parts = null;
                return false;
            }
            parts = new UserInputExpressionParts
            {
                Bindings = memberInitExpression.Bindings.ToArray(),
                ModelCreateExpression = memberInitExpression.NewExpression
            };
            return true;
        }
    }
}