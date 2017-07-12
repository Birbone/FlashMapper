using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface ISpecificPropertyAssignMapExpressionBuilderFactory
    {
        bool TryCreate(Expression leftOperand, Expression rightOperand, out IMapExpressionBuilder builder);
    }
}