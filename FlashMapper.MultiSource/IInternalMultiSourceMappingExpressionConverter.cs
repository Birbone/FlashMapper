using System;
using System.Linq.Expressions;

namespace FlashMapper.MultiSource
{
    public interface IInternalMultiSourceMappingExpressionConverter
    {
        Expression<Func<TWrap, TDestination>> Convert<TWrap, TDestination>(LambdaExpression mappingExpression);
    }
}