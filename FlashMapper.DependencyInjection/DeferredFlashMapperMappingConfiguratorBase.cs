using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Internal.Utils;
using FlashMapper.Models;
using FlashMapper.Services;

namespace FlashMapper.DependencyInjection
{
    internal abstract class DeferredFlashMapperMappingConfiguratorBase<TConfigurator, TInternalConfigurator> :
        IFlashMapperSettingsBuilder<TConfigurator>, IFlashMapperCustomServiceBuilder<TConfigurator>
        where TConfigurator : IFlashMapperSettingsBuilder<TConfigurator>, IFlashMapperCustomServiceBuilder<TConfigurator>
        where TInternalConfigurator : IFlashMapperSettingsBuilder<TInternalConfigurator>, IFlashMapperCustomServiceBuilder<TInternalConfigurator>
    {
        private readonly ParameterExpression builderExpressionParameter;
        private Expression builderExpression;
        private TInternalConfigurator internalBuilder;

        protected DeferredFlashMapperMappingConfiguratorBase()
        {
            builderExpression = builderExpressionParameter = Expression.Parameter(typeof(TInternalConfigurator));
        }

        public TInternalConfigurator Initialize(TInternalConfigurator internalBuilder)
        {
            this.internalBuilder = internalBuilder;
            var builderMethod = Expression.Lambda<Func<TInternalConfigurator, TInternalConfigurator>>(builderExpression,
                builderExpressionParameter).Compile();
            return builderMethod(internalBuilder);
        }

        public TConfigurator UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior)
        {
            return ApplyBuilderMethod(nameof(UnresolvedBehavior), ma(unresolvedPropertyBehavior));
        }

        public TConfigurator CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior)
        {
            return ApplyBuilderMethod(nameof(CollisionBehavior), ma(selectSourceCollisionBehavior));
        }

        public TConfigurator SourceNamingConvention(INamingConvention namingConvention)
        {
            return ApplyBuilderMethod(nameof(SourceNamingConvention), ma(namingConvention));
        }

        public TConfigurator SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            return ApplyBuilderMethod(nameof(SourceNamingConvention), ma(namingConventionType), ma(prefixes));
        }

        public TConfigurator DestinationNamingConvention(INamingConvention namingConvention)
        {
            return ApplyBuilderMethod(nameof(DestinationNamingConvention), ma(namingConvention));
        }

        public TConfigurator DestinationNamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            return ApplyBuilderMethod(nameof(DestinationNamingConvention), ma(namingConventionType), ma(prefixes));
        }

        private static TInternalConfigurator RegisterServiceInternal<TService>(
            TInternalConfigurator internalConfigurator,
            Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService
        {
            return internalConfigurator.RegisterService(serviceRegistration);
        }

        public TConfigurator RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService
        {
            Func<TInternalConfigurator, Func<IFlashMapperDependencyResolver, TService>, TInternalConfigurator> method = RegisterServiceInternal;
            if (internalBuilder == null)
            {
                builderExpression = Expression.Call(method.Method, builderExpression,
                    Expression.Constant(serviceRegistration, typeof(Func<IFlashMapperDependencyResolver, TService>)));
            }
            else
            {
                method(internalBuilder, serviceRegistration);
            }
            return This;
        }

        protected class MethodArgument
        {
            public MethodArgument(object value, Type type)
            {
                Value = value;
                Type = type;
            }

            public object Value { get; }
            public Type Type { get; }
        }

        protected MethodArgument ma<TValue>(TValue value)
        {
            return new MethodArgument(value, typeof(TValue));
        }

        protected MethodInfo GetBuilderMethod(string methodName, params Type[] methodArguments)
        {
            return typeof(TInternalConfigurator).GetMethods()
                .Union(typeof(TInternalConfigurator).GetInterfaces().SelectMany(i => i.GetMethods()))
                .First(m => m.Name == methodName &&
                            m.GetParameters().Length == methodArguments.Length &&
                            m.GetParameters()
                                .Zip(methodArguments, (p, e) => p.ParameterType == e)
                                .All(b => b));
        }

        protected MethodInfo GetBuilderMethod<TGenericArgument>(string methodName, params Type[] methodArguments)
        {
            var aa = typeof(TInternalConfigurator).GetMethods()
                .Union(typeof(TInternalConfigurator).GetInterfaces().SelectMany(i => i.GetMethods()))
                .ToArray();
            var bb = aa.Where(m => m.Name == methodName).ToArray();
            var cc = bb.Where(m => m.IsGenericMethod).ToArray();
            var dd = cc.Where(m => m.GetGenericArguments().Length == 1).ToArray();
            var ee = dd.Where(m => m.GetParameters().Length == methodArguments.Length).ToArray();
            var ff = ee.Where(m => m.GetParameters().Zip(methodArguments, (p, e) => p.ParameterType == e).All(b =>b)).ToArray();


            return typeof(TInternalConfigurator).GetMethods()
                .Union(typeof(TInternalConfigurator).GetInterfaces().SelectMany(i => i.GetMethods()))
                .First(m => m.Name == methodName &&
                            m.IsGenericMethod &&
                            m.GetGenericArguments().Length == 1 &&
                            m.GetParameters().Length == methodArguments.Length &&
                            m.GetParameters()
                                .Zip(methodArguments, (p, e) => p.ParameterType == e)
                                .All(b => b))
                .MakeGenericMethod(typeof(TGenericArgument));
        }

        protected void ApplyBuilderMethod(MethodInfo builderMethod, params MethodArgument[] arguments)
        {
            if (internalBuilder == null)
            {
                builderExpression = Expression.Call(builderExpression, builderMethod,
                    arguments.Select(a => Expression.Constant(a.Value, a.Type)));
            }
            else
            {
                builderMethod.Invoke(internalBuilder, arguments.Select(a => a.Value).ToArray());
            }
        }

        protected TConfigurator ApplyBuilderMethod(string methodName, params MethodArgument[] methodArguments)
        {
            var method = GetBuilderMethod(methodName, methodArguments.Select(a => a.Type).ToArray());
            ApplyBuilderMethod(method, methodArguments);
            return This;
        }

        protected TConfigurator ApplyBuilderMethod<TGenericArgument>(string methodName, params MethodArgument[] methodArguments)
        {
            var method = GetBuilderMethod<TGenericArgument>(methodName, methodArguments.Select(a => a.Type).ToArray());
            ApplyBuilderMethod(method, methodArguments);
            return This;
        }

        protected abstract TConfigurator This { get; }
    }
}