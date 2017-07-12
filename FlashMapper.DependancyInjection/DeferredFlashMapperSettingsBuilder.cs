using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;

namespace FlashMapper.DependancyInjection
{
    public class DeferredFlashMapperSettingsBuilder: IFlashMapperSettingsBuilder
    {
        private readonly ParameterExpression builderExpressionParameter;
        private Expression builderExpression;
        private IFlashMapperSettingsBuilder internalBuilder;
        public DeferredFlashMapperSettingsBuilder()
        {
            builderExpression = builderExpressionParameter = Expression.Parameter(typeof(IFlashMapperSettingsBuilder));
        }

        public IFlashMapperSettingsBuilder Initialize(IFlashMapperSettingsBuilder internalBuilder)
        {
            this.internalBuilder = internalBuilder;
            return this;
        }

        private class MethodArgument
        {
            public MethodArgument(object value, Type type)
            {
                Value = value;
                Type = type;
            }

            public object Value { get; }
            public Type Type { get; }
        }

        private MethodArgument ma<TValue>(TValue value)
        {
            return new MethodArgument(value, typeof(TValue));
        }

        private MethodInfo GetBuilderMethod(string methodName, params Type[] methodArguments)
        {
            return typeof(IFlashMapperSettingsBuilder).GetMethod(methodName, methodArguments);
        }

        private void ApplyBuilderMethod(MethodInfo builderMethod, params MethodArgument[] arguments)
        {
            builderExpression = Expression.Call(builderExpression, builderMethod, arguments.Select(a => Expression.Constant(a.Value, a.Type)));
        }
        
        private IFlashMapperSettingsBuilder ApplyBuilderMethod(string methodName, params MethodArgument[] methodArguments)
        {
            var method = GetBuilderMethod(methodName, methodArguments.Select(a => a.Type).ToArray());
            ApplyBuilderMethod(method, methodArguments);
            return this;
        }

        public IFlashMapperSettingsBuilder UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior)
        {
            return ApplyBuilderMethod(nameof(UnresolvedBehavior), ma(unresolvedPropertyBehavior));
        }

        public IFlashMapperSettingsBuilder CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior)
        {
            return ApplyBuilderMethod(nameof(CollisionBehavior), ma(selectSourceCollisionBehavior));
        }

        public IFlashMapperSettingsBuilder SourceNamingConvention(INamingConvention namingConvention)
        {
            return ApplyBuilderMethod(nameof(SourceNamingConvention), ma(namingConvention));
        }

        public IFlashMapperSettingsBuilder SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            return ApplyBuilderMethod(nameof(SourceNamingConvention), ma(namingConventionType), ma(prefixes));
        }

        public IFlashMapperSettingsBuilder DestinationNamingConvention(INamingConvention namingConvention)
        {
            return ApplyBuilderMethod(nameof(DestinationNamingConvention), ma(namingConvention));
        }

        public IFlashMapperSettingsBuilder DestinationNamingConvention(NamingConventionType namingConventionType,
            params string[] prefixes)
        {
            return ApplyBuilderMethod(nameof(DestinationNamingConvention), ma(namingConventionType), ma(prefixes));
        }
        
        public IFlashMapperSettings GetSettings()
        {
            if (internalBuilder == null)
                throw new FlashMapperException("DeferredSettingsBuilder: Internal settings builder is not initialized.");
            var builderMethod = Expression.Lambda<Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder>>(builderExpression,
                builderExpressionParameter).Compile();
            return builderMethod(internalBuilder).GetSettings();
        }
    }
}