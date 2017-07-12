using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface ISpecificIgnoreNodeChecker
    {
        bool IsIgnoreNode(Expression node);
    }
}