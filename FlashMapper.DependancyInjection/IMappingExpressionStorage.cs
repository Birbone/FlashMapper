using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FlashMapper.DependancyInjection
{
    internal interface IMappingExpressionStorageProvider
    {
        IMappingExpressionStorage GetExpressionStorage();
    }

    internal interface IMappingExpressionStorage
    {
        LambdaExpression GetMappingExpression();
    }

    internal class MappingExpressionStorage : IMappingExpressionStorage
    {
        private readonly LambdaExpression mappingExpression;
        public MappingExpressionStorage(LambdaExpression mappingExpression)
        {
            this.mappingExpression = mappingExpression;
        }

        public LambdaExpression GetMappingExpression()
        {
            return mappingExpression;
        }
    }

    internal class ExtraParameterResolveMap
    {
        public ExtraParameterResolveMap(ConstantExpression parameterPlaceholder, LambdaExpression resolveValueMethod)
        {
            ParameterPlaceholder = parameterPlaceholder;
            ResolveValueMethod = resolveValueMethod;
        }

        public ConstantExpression ParameterPlaceholder { get; }
        public LambdaExpression ResolveValueMethod { get; }
    }
    
    internal class MappingExpressionStorageWithExtraParameters : IMappingExpressionStorage
    {
        private readonly IMappingExpressionStorageProvider innerStorageProvider;
        private readonly LambdaExpression lastParameterResolveMethod;

        public MappingExpressionStorageWithExtraParameters(IMappingExpressionStorageProvider innerStorageProvider, LambdaExpression lastParameterResolveMethod)
        {
            this.innerStorageProvider = innerStorageProvider;
            this.lastParameterResolveMethod = lastParameterResolveMethod;
        }

        public LambdaExpression GetMappingExpression()
        {
            var innerExpressionStorage = innerStorageProvider.GetExpressionStorage();
            var innerExpression = innerExpressionStorage.GetMappingExpression();
            var innerExpressionParams = innerExpression.Parameters;
            if (innerExpressionParams.Count < 1)
                throw new Exception("Cannot replace last parameter of the expression with method because expression does not have any parameters.");
            var lastParameter = innerExpressionParams[innerExpressionParams.Count - 1];
            var lastParameterVariable = Expression.Variable(lastParameter.Type);
            var parametersReplacementMap = innerExpressionParams.Zip(lastParameterResolveMethod.Parameters.With(lastParameterVariable), 
                (s, r) => new ExpressionReplacement(s, r));
            var expressionReplacementVisitor = new ExpressionReplacementVisitor(parametersReplacementMap);
            var resultInnerBody = expressionReplacementVisitor.Visit(innerExpression.Body);
            var lastParameterResolveMethodInvokation = Expression.Invoke(lastParameterResolveMethod, lastParameterResolveMethod.Parameters);
            var lastParameterVariableAssign = Expression.Assign(lastParameterVariable, lastParameterResolveMethodInvokation);
            var resultBody = Expression.Block(new[] {lastParameterVariable}, lastParameterVariableAssign, resultInnerBody);
            return Expression.Lambda(resultBody, lastParameterResolveMethod.Parameters);
        }
    }
}
