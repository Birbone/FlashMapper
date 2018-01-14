using System.Linq.Expressions;
using FlashMapper.Models;

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
        /// <param name="context"></param>
        /// <returns></returns>
        Expression Process(Expression inputExpression, MappingPostProcessingContext context);
    }
}