using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that checks if current expression node is Ignore marker
    /// </summary>
    public interface ISpecificIgnoreNodeChecker
    {
        /// <summary>
        /// Returns if <see cref="node"/> is Ignore marker
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsIgnoreNode(Expression node);
    }
}