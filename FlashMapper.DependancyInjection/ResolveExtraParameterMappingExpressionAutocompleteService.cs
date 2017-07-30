using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.DependancyInjection
{
    internal class ResolveExtraParameterMappingExpressionAutocompleteService: IMappingExpressionAutocompleteService
    {
        private void CheckMarker(Expression expression, string marker)
        {
            var expressionMarker = expression as ConstantExpression;
            if (expressionMarker?.Value as string != marker)
                throw new Exception($"ExtraParameter expression must have a marker at this point - {marker}.");
        }

        private ResolveExtraParameterBlocks ParseExpression(Expression inputExpressionBody)
        {
            var inputExpressionBodyBlock = inputExpressionBody as BlockExpression;
            if (inputExpressionBodyBlock == null)
                throw new Exception("ExtraParameter expression's body must be block.");
            var actions = new Stack<Expression>(inputExpressionBodyBlock.Expressions.Reverse());
            CheckMarker(actions.Pop(), ResolveExtraParameterExpressionMarkers.ResolveExtraParameterCall);
            var result = new ResolveExtraParameterBlocks();
            var resolveExtraParameterAssign = actions.Pop() as BinaryExpression;
            if (resolveExtraParameterAssign == null || resolveExtraParameterAssign.NodeType != ExpressionType.Assign)
                throw new Exception("ExtraParameter initialization was expected.");
            var extraParameterVariable = resolveExtraParameterAssign.Left as ParameterExpression;
            if (extraParameterVariable == null)
                throw new Exception("Initialization of extra parameter was expected.");
            result.ExtraParameterVariable = extraParameterVariable;
            result.ExtraParameterInitialization = resolveExtraParameterAssign;
            CheckMarker(actions.Pop(), ResolveExtraParameterExpressionMarkers.ConvertNextStepCall);
            var resultAssign = actions.Pop() as BinaryExpression;
            if (resultAssign == null || resultAssign.NodeType != ExpressionType.Assign)
                throw new Exception("Result initialization was expected.");
            var resultVariable = resultAssign.Left as ParameterExpression;
            if (resultVariable == null)
                throw new Exception("Result initialization was expected.");
            result.ResultVariable = resultVariable;
            result.ConvertNextStepCall = resultAssign.Right;
            CheckMarker(actions.Pop(), ResolveExtraParameterExpressionMarkers.MapNextStepCall);
            result.MapNextStepCall = actions.Pop();
            return result;
        }

        public Expression<Func<TSource, TDestination>> CompleteBuildExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression, 
            IFlashMapperSettings settings)
        {
            var expressionBlocks = ParseExpression(inputExpression.Body);
            var resultExpressionBody = Expression.Block(new[] {expressionBlocks.ExtraParameterVariable},
                expressionBlocks.ExtraParameterInitialization, expressionBlocks.ConvertNextStepCall);
            return Expression.Lambda<Func<TSource, TDestination>>(resultExpressionBody, inputExpression.Parameters);
        }

        public Expression<Action<TSource, TDestination>> CompleteMapDataExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression, 
            IFlashMapperSettings settings)
        {
            var expressionBlocks = ParseExpression(inputExpression.Body);
            var resultExpressionBody = Expression.Block(new[] { expressionBlocks.ExtraParameterVariable },
                expressionBlocks.ExtraParameterInitialization, expressionBlocks.MapNextStepCall);
            return Expression.Lambda<Action<TSource, TDestination>>(resultExpressionBody,
                inputExpression.Parameters.With(expressionBlocks.ResultVariable));
        }
    }
}