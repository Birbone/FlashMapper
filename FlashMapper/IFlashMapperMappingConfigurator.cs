using System;
using System.Linq;
using FlashMapper.Internal.Utils;
using FlashMapper.Models;
using FlashMapper.Services;

namespace FlashMapper
{
    public interface IFlashMapperMappingConfigurator<out TSource, out TDestination> : 
        IFlashMapperSettingsBuilder<IFlashMapperMappingConfigurator<TSource, TDestination>>,
        IFlashMapperMappingCallbacksBuilder<TSource, TDestination, IFlashMapperMappingConfigurator<TSource, TDestination>>, 
        IFlashMapperCustomServiceBuilder<IFlashMapperMappingConfigurator<TSource, TDestination>>
    {
    }

    public class FlashMapperMappingConfigurator<TSource, TDestination> : IFlashMapperMappingConfigurator<TSource, TDestination>
    {
        private UnresolvedPropertyBehavior unresolvedPropertyBehavior;
        private SelectSourceCollisionBehavior selectSourceCollisionBehavior;
        private INamingConvention sourceNamingConvention;
        private INamingConvention destinationNamingConvention;
        private Action<TSource, TDestination> afterMapCallback;
        private Action<TSource, TDestination> beforeMapCallback;
        private readonly FlashMapperDependencyResolver dependencyResolver;

        public FlashMapperMappingConfigurator(IFlashMapperDependencyResolver internalDependencyResolver)
        {
            this.unresolvedPropertyBehavior = UnresolvedPropertyBehavior.Inherit;
            this.selectSourceCollisionBehavior = SelectSourceCollisionBehavior.Inherit;
            this.sourceNamingConvention = null;
            this.destinationNamingConvention = null;
            this.afterMapCallback = null;
            this.beforeMapCallback = null;
            dependencyResolver = new FlashMapperDependencyResolver(internalDependencyResolver, Enumerable.Empty<IFlashMapperService>());
        }
        public IFlashMapperMappingConfigurator<TSource, TDestination> UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior)
        {
            this.unresolvedPropertyBehavior = unresolvedPropertyBehavior;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior)
        {
            this.selectSourceCollisionBehavior = selectSourceCollisionBehavior;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> SourceNamingConvention(INamingConvention namingConvention)
        {
            this.sourceNamingConvention = namingConvention;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            return SourceNamingConvention(new NamingConvention(namingConventionType, prefixes));
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> DestinationNamingConvention(INamingConvention namingConvention)
        {
            this.destinationNamingConvention = namingConvention;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> DestinationNamingConvention(NamingConventionType namingConventionType,
            params string[] prefixes)
        {
            return DestinationNamingConvention(new NamingConvention(namingConventionType, prefixes));
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterMapCallback)
        {
            this.afterMapCallback = afterMapCallback;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeMapCallback)
        {
            this.beforeMapCallback = beforeMapCallback;
            return this;
        }

        public IFlashMapperMappingConfigurator<TSource, TDestination> RegisterService<TService>(Func<IFlashMapperDependencyResolver, TService> serviceRegistration) where TService : class, IFlashMapperService
        {
            dependencyResolver.RegisterService(serviceRegistration);
            return this;
        }

        public IFlashMapperDependencyResolver GetResultDependencyResolver()
        {
            return dependencyResolver;
        }

        public IFlashMapperSettings GetSettings()
        {
            return new FlashMapperSettings(unresolvedPropertyBehavior, selectSourceCollisionBehavior,
                new MapNamingConventionSettings(sourceNamingConvention, destinationNamingConvention));
        }

        public IFlashMapperMappingCallbacks<TSource, TDestination> GetMappingCallbacks()
        {
            return new FlashMapperMappingCallbacks<TSource, TDestination>(afterMapCallback, beforeMapCallback);
        }
    }
}