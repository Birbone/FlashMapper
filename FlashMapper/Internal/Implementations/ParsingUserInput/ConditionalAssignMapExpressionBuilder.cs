using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class ConditionalAssignMapExpressionBuilder : IMapExpressionBuilder
    {
        private readonly Expression condition;
        private readonly Expression leftOperand;
        private readonly Expression rightOperand;

        public ConditionalAssignMapExpressionBuilder(Expression condition, Expression leftOperand, Expression rightOperand)
        {
            this.condition = condition;
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public Expression GetExpression()
        {
            return Expression.IfThen(condition, Expression.Assign(leftOperand, rightOperand));
        }
    }
}