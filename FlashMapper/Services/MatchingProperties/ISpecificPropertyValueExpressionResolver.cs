using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;

namespace FlashMapper.Services.MatchingProperties
{
    public interface ISpecificPropertyValueExpressionResolver
    {
        bool TryGetPropertyValueExpression<TSource, TDestination>(ParameterExpression source, 
            PropertyInfo property, 
            MemberBinding[] userBindings, 
            out Expression propertyValueExpression);
    }
}