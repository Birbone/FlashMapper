using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;

namespace FlashMapper.Services.GeneratingMappings
{
    public interface IPropertyValueExpressionResolver: IFlashMapperService
    {
        Expression GetPropertyValueExpression<TSource, TDestination>(ParameterExpression source, PropertyInfo property, MemberBinding[] userBindings, IFlashMapperSettings modelMapperSettings);
    }
}