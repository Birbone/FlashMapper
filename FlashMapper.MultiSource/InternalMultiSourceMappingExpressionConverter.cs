using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlashMapper.MultiSource
{
    public class InternalMultiSourceMappingExpressionConverter : IInternalMultiSourceMappingExpressionConverter
    {
        public Expression<Func<TWrap, TDestination>> Convert<TWrap, TDestination>(LambdaExpression mappingExpression)
        {
            var wrapType = typeof(TWrap);
            var sourceParameters = mappingExpression.Parameters;
            var wrapParameter = Expression.Parameter(wrapType);
            var parameterPropertyMap = sourceParameters.Select((s, i) => new
            {
                Parameter = s,
                WrapProperty = wrapType.GetProperty("Source" + (i + 1))
            }).ToDictionary(p => p.Parameter, p => (Expression)Expression.Property(wrapParameter, p.WrapProperty));
            var visitor = new ParameterReplacementVisitor(parameterPropertyMap);
            var resultMethodBody = visitor.Visit(mappingExpression.Body);
            return Expression.Lambda<Func<TWrap, TDestination>>(resultMethodBody, wrapParameter);
        }
    }
}