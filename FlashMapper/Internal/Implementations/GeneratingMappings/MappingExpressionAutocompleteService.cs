using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class MappingExpressionAutocompleteService : IMappingExpressionAutocompleteService
    {
        private readonly IPropertyValueExpressionResolver propertyValueExpressionResolver;
        private readonly IUserInputParser userInputParser;
        private readonly IMapExpressionPostProcessor mapExpressionPostProcessor;
        
        public MappingExpressionAutocompleteService(IPropertyValueExpressionResolver propertyValueExpressionResolver, 
            IUserInputParser userInputParser,
            IMapExpressionPostProcessor mapExpressionPostProcessor)
        {
            this.propertyValueExpressionResolver = propertyValueExpressionResolver;
            this.userInputParser = userInputParser;
            this.mapExpressionPostProcessor = mapExpressionPostProcessor;
        }
        
        public Expression<Func<TSource, TDestination>> CompleteBuildExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression, IFlashMapperMappingCallbacks<TSource, TDestination> callbacks)
        {
            var destinationType = typeof(TDestination);
            var source = inputExpression.Parameters[0];
            var destination = Expression.Variable(destinationType);
            var userInputParts = userInputParser.GetUserInputParts(inputExpression.Body);
            var assignDestination = Expression.Assign(destination, userInputParts.ModelCreateExpression);
            var actions = new List<Expression>
            {
                assignDestination
            };
            actions.Add(GetDelegateInvocationExpression(callbacks.BeforeMapCallback, source, destination));
            actions.AddRange(GetPropertyAssigns<TSource, TDestination>(source, destination, userInputParts.Bindings));
            actions.Add(GetDelegateInvocationExpression(callbacks.AfterMapCallback, source, destination));
            actions.Add(destination);
            var methodBody = Expression.Block(new[] {destination}, actions);
            var context = new MappingPostProcessingContext(true);
            var processedBody = mapExpressionPostProcessor.Process(methodBody, context);
            var resultLambda = Expression.Lambda<Func<TSource, TDestination>>(processedBody, source);
            return resultLambda;
        }
        
        public Expression<Action<TSource, TDestination>> CompleteMapDataExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression, IFlashMapperMappingCallbacks<TSource, TDestination> callbacks)
        {
            var destinationType = typeof(TDestination);
            var source = inputExpression.Parameters[0];
            var destination = Expression.Parameter(destinationType);
            var userInputParts = userInputParser.GetUserInputParts(inputExpression.Body);
            var actions = new List<Expression>();
            actions.Add(GetDelegateInvocationExpression(callbacks.BeforeMapCallback, source, destination));
            actions.AddRange(GetPropertyAssigns<TSource, TDestination>(source, destination, userInputParts.Bindings));
            actions.Add(GetDelegateInvocationExpression(callbacks.AfterMapCallback, source, destination));
            var methodBody = Expression.Block(actions);
            var context = new MappingPostProcessingContext(false);
            var processedBody = mapExpressionPostProcessor.Process(methodBody, context);
            var resultLambda = Expression.Lambda<Action<TSource, TDestination>>(processedBody, source, destination);
            return resultLambda;
        }

        private Expression GetDelegateInvocationExpression(Delegate @delegate, params Expression[] arguments)
        {
            if (@delegate == null)
                return Expression.Empty();
            if (@delegate.Target == null)
                return Expression.Call(@delegate.Method, arguments);
            return Expression.Call(Expression.Constant(@delegate.Target), @delegate.Method, arguments);
        }

        private IEnumerable<Expression> GetPropertyAssigns<TSource, TDestination>(ParameterExpression source, 
            ParameterExpression destination, 
            MemberBinding[] userInputBindings)
        {
            var destinationType = typeof(TDestination);
            var propertiesToSet = destinationType.GetProperties()
                .Where(p => p.CanWrite)
                .ToArray();
            foreach (var property in propertiesToSet)
            {
                var propertyExpression = propertyValueExpressionResolver.GetPropertyValueExpression<TSource, TDestination>(source, property,
                    userInputBindings);
                var destinationProperty = Expression.Property(destination, property);
                var destinationPropertyAssign = Expression.Assign(destinationProperty, propertyExpression);
                yield return destinationPropertyAssign;
            }
            if (propertiesToSet.Length == 0)
                yield return Expression.Empty();
        }
    }
}