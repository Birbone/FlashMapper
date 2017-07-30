using System.Linq.Expressions;

namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Map expression post processor service
    /// </summary>
    public interface ISpecificMapExpressionPostProcessor
    {
        /// <summary>
        /// Make some changes into expression after autocompletion
        /// </summary>
        /// <param name="inputExpression"></param>
        /// <returns></returns>
        Expression Process(Expression inputExpression);
    }
}