//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using FlashMapper.Models;
//using FlashMapper.Services.GeneratingMappings;

//namespace FlashMapper.DependancyInjection
//{
//    internal class MappingExpressionCofiguratorContext
//    {
//        public MappingExpressionCofiguratorContext(IFlashMapperSettingsBuilder modelMapperSettingsBuilder)
//        {
//            ModelMapperSettingsBuilder = modelMapperSettingsBuilder;
//            ExtraParameterResolvers = new List<MethodInfo>();
//        }

//        public IFlashMapperSettingsBuilder ModelMapperSettingsBuilder { get; }
//        public List<MethodInfo> ExtraParameterResolvers { get; }
//        public LambdaExpression MainExpression { get; set; }
//    }

//    internal interface IMappingExpressionResolver
//    {
//        LambdaExpression GetMappingExpression(object builderInstance, Type[] originalParameters, IEnumerable<MethodInfo> extraParameterResolvers, LambdaExpression mainExpression);
//    }

//    internal class MappingExpressionResolver : IMappingExpressionResolver
//    {
//        public LambdaExpression GetMappingExpression(object builderInstance, Type[] originalParameters, IEnumerable<MethodInfo> extraParameterResolvers, LambdaExpression mainExpression)
//        {
//            var extraParametersResolversArray = extraParameterResolvers.ToArray();
//            if (extraParametersResolversArray.Length == 0)
//                return mainExpression;

//            var resultParameters = originalParameters.Select(Expression.Parameter)
//                .ToList();
//            var allParameters = new List<Expression>(resultParameters);
//            var variables = new List<ParameterExpression>();
//            var expressionActions = new List<Expression>
//            {
//                Expression.Constant(ExpressionParserMarkers.ExtraParametersExpression)
//            };
//            var builder = Expression.Constant(builderInstance);
//            foreach (var extraParameterResolver in extraParametersResolversArray)
//            {
//                var extraParameter = Expression.Variable(extraParameterResolver.ReturnType);
//                variables.Add(extraParameter);
//                var extraParameterResolverCall = Expression.Call(builder, extraParameterResolver, allParameters);
//                var extraParameterAssign = Expression.Assign(extraParameter, extraParameterResolverCall);
//                expressionActions.Add(extraParameterAssign);
//                allParameters.Add(extraParameter);
//            }
//            expressionActions.Add(Expression.Constant(ExpressionParserMarkers.MainExpression));
//            var mainExpressionBuilderParameter = Expression.Parameter(builderInstance.GetType());
//            var toStaticConverter = new ToStaticMethodVisitor(builder, mainExpressionBuilderParameter);
//            var staticMainExpressionBody = toStaticConverter.Visit(mainExpression.Body);
//            var staticMainExpression = Expression.Lambda(staticMainExpressionBody, mainExpression.Parameters.With(mainExpressionBuilderParameter));
//            allParameters.Add(builder);
//            var mainExpressionInvokation = Expression.Invoke(staticMainExpression, allParameters);
//            expressionActions.Add(mainExpressionInvokation);
//            var resultExpressionBody = Expression.Block(variables, expressionActions);
//            return Expression.Lambda(resultExpressionBody, resultParameters);
//        }
//    }

//    internal class ExtraParametersMappingExpressionAutocompleteService: IMappingExpressionAutocompleteService
//    {
//        private readonly IMappingExpressionAutocompleteService originalAutocompleteService;

//        public ExtraParametersMappingExpressionAutocompleteService(IMappingExpressionAutocompleteService originalAutocompleteService)
//        {
//            this.originalAutocompleteService = originalAutocompleteService;
//        }

//        private bool IsExtraParametersExpression(Expression lambdaBody)
//        {
//            var blockLambdaBody = lambdaBody as BlockExpression;
//            if (blockLambdaBody == null)
//                return false;
//            var marker = blockLambdaBody.Expressions.FirstOrDefault() as ConstantExpression;
//            if (marker == null)
//                return false;
//            return Equals(ExpressionParserMarkers.ExtraParametersExpression, marker.Value);
//        }

//        public Expression<Func<TSource, TDestination>> CompleteBuildExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression,
//            IFlashMapperSettings settings)
//        {
//            if (!IsExtraParametersExpression(inputExpression.Body))
//                return originalAutocompleteService.CompleteBuildExpression(inputExpression, settings);

//            throw new NotImplementedException();
//        }

//        public Expression<Action<TSource, TDestination>> CompleteMapDataExpression<TSource, TDestination>(Expression<Func<TSource, TDestination>> inputExpression,
//            IFlashMapperSettings settings)
//        {
//            if (!IsExtraParametersExpression(inputExpression.Body))
//                return originalAutocompleteService.CompleteMapDataExpression(inputExpression, settings);

//            throw new NotImplementedException();
//        }
//    }

//    internal class ExtraParametersMappingExpressionAutocompleteServiceVisitor : ExpressionVisitor
//    {
//        private bool mainExpressionMarkerHit;

//        protected override Expression VisitConstant(ConstantExpression node)
//        {
//            if (Equals(node.Value, ExpressionParserMarkers.ExtraParametersExpression))
//            {
//                mainExpressionMarkerHit = false;
//                return Expression.Empty();
//            }

//            if (Equals(node.Value, ExpressionParserMarkers.MainExpression))
//            {

//                mainExpressionMarkerHit = true;
//                return Expression.Empty();
//            }
//            return base.VisitConstant(node);
//        }

//        protected override Expression VisitInvocation(InvocationExpression node)
//        {
//            if (!mainExpressionMarkerHit)
//                return base.VisitInvocation(node);

//        }
//    }

//    internal static class ExpressionParserMarkers
//    {
//        public static readonly string ExtraParametersExpression = $"{nameof(ExtraParametersExpression)}_{Guid.NewGuid():d}";
//        public static readonly string MainExpression = $"{nameof(MainExpression)}_{Guid.NewGuid():d}";
//    }
//}
