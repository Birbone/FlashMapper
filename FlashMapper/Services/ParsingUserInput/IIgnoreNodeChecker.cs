using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    public interface IIgnoreNodeChecker: IFlashMapperService
    {
        bool IsIgnoreNode(Expression node);
    }
}