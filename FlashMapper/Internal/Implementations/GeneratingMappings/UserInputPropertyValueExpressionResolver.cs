using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class UserInputPropertyValueExpressionResolver : IUserInputPropertyValueExpressionResolver
    {
        public bool TryGetPropertyValueExpression<TSource, TDestination>(ParameterExpression source, 
            PropertyInfo property, 
            MemberBinding[] userBindings, 
            out Expression propertyValueExpression)
        {
            var userDefinedBinding = userBindings.FirstOrDefault(b => b.Member.Name == property.Name);
            if (userDefinedBinding == null)
            {
                propertyValueExpression = null;
                return false;
            }
            var memberAssignmentBinding = userDefinedBinding as MemberAssignment;
            if (memberAssignmentBinding != null)
            {
                propertyValueExpression = memberAssignmentBinding.Expression;
                return true;
            }
            propertyValueExpression = null;
            return false;
        }
    }
}