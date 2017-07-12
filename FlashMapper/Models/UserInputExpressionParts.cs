using System.Linq.Expressions;

namespace FlashMapper.Models
{
    public class UserInputExpressionParts
    {
        public MemberBinding[] Bindings { get; set; }
        public NewExpression ModelCreateExpression { get; set; }
    }
}