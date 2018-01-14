using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that calls all <see cref="ISpecificMapExpressionPostProcessor"/> on expression
    /// </summary>
    public interface IMapExpressionPostProcessor: IFlashMapperService
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