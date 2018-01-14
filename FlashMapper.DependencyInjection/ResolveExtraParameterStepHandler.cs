using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.DependencyInjection
{
    internal class ResolveExtraParameterStepHandler : ISpecificMappingConfigStepHandler
    {
        private LambdaExpression MakeExpressionStatic<TBuilder>(LambdaExpression inputExpression, TBuilder builderInstance)
        {
            var builderParameter = Expression.Parameter(typeof(TBuilder));
            var toStaticMethodVisitor = new ToStaticMethodVisitor(builderInstance, builderParameter);
            var staticMethodBody = toStaticMethodVisitor.Visit(inputExpression.Body);
            return Expression.Lambda(staticMethodBody,
                inputExpression.Parameters.With(builderParameter));
        }

        public bool TryProcessStep<TBuilder>(IMappingConfigStep step, TBuilder builder, IMappingConfiguration currentMappingConfiguration, IMappingConfiguration previousMappingConfiguration)
        {
            if (!(step is ResolveExtraParameterStep resolveExtraParameterStep))
                return false;
            var methodParameters = resolveExtraParameterStep.ResolveParameterExpression.Parameters
                .Select(p => Expression.Parameter(p.Type, p.Name))
                .ToList();
            var methodActions = new List<Expression>
            {
                Expression.Constant(ResolveExtraParameterExpressionMarkers.ResolveExtraParameterCall)
            };
            var builderParameter = Expression.Parameter(typeof(TBuilder));
            var newParameter = Expression.Variable(resolveExtraParameterStep.ResolveParameterExpression.ReturnType);
            var staticResolveParameterExpression = MakeExpressionStatic(resolveExtraParameterStep.ResolveParameterExpression, builder);
            var newParameterResolveCall = Expression.Invoke(staticResolveParameterExpression, methodParameters.With(builderParameter));
            var newParameterAssign = Expression.Assign(newParameter, newParameterResolveCall);
            methodActions.Add(newParameterAssign);
            var result = Expression.Variable(resolveExtraParameterStep.NextStepConvertMethod.ReturnType);
            var callNextStepConvert = Expression.Call(null, resolveExtraParameterStep.NextStepConvertMethod,
                methodParameters
                    .WithBefore<Expression>(Expression.Constant(previousMappingConfiguration))
                    .With(newParameter)
                    .With(builderParameter));

            var assignResult = Expression.Assign(result, callNextStepConvert);
            methodActions.Add(Expression.Constant(ResolveExtraParameterExpressionMarkers.ConvertNextStepCall));
            methodActions.Add(assignResult);
            var callNextStepMapData = Expression.Call(null, resolveExtraParameterStep.NextStepMapMethod,
                methodParameters
                    .WithBefore<Expression>(Expression.Constant(previousMappingConfiguration))
                    .With(newParameter)
                    .With(builderParameter)
                    .With(result));
            methodActions.Add(Expression.Constant(ResolveExtraParameterExpressionMarkers.MapNextStepCall));
            methodActions.Add(callNextStepMapData);
            methodActions.Add(result);
            var methodBody = Expression.Block(new[] {newParameter, result}, methodActions);
            var mapExpression = Expression.Lambda(methodBody, methodParameters.With(builderParameter));
            resolveExtraParameterStep.ServiceRegistrationHelper.RegisterService<IMappingExpressionAutocompleteService>(r => new ResolveExtraParameterMappingExpressionAutocompleteService());
            resolveExtraParameterStep.CreateMappingMethod.Invoke(null, new object[]
            {
                currentMappingConfiguration,
                mapExpression,
                resolveExtraParameterStep.ServiceRegistrationHelper.GetInitializationDelegate()
            });
            return true;
        }
    }
}