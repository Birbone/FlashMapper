using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Internal;
using FlashMapper.Internal.Utils;
using FlashMapper.Models;
using FlashMapper.Services;
using FlashMapper.Services.GeneratingMappings;
using FlashMapper.Services.Settings;

namespace FlashMapper
{
    public sealed class MappingConfiguration : IMappingConfiguration
    {
        private readonly IEnumerable<IFlashMapperService> customServices;
        private readonly IFlashMapperDependencyResolver dependencyResolver;
        private readonly IMappingsStorage mappingsStorage;
        private readonly List<IMappingConfiguration> dependantConfigurations;

        public MappingConfiguration(): this(Enumerable.Empty<IFlashMapperService>())
        {
        }

        public MappingConfiguration(IEnumerable<IFlashMapperService> customServices)
        {
            var customServicesArray = customServices.ToArray();
            this.customServices = customServicesArray;
            InstanceId = Guid.NewGuid();

            dependencyResolver = customServicesArray.Length > 0
                ? new FlashMapperDependencyResolver(ModuleConfiguration.GetDefaultResolver(), customServicesArray)
                : ModuleConfiguration.GetDefaultResolver();

            mappingsStorage = dependencyResolver.GetService<IMappingsStorageFactory>().Create(this);
            dependantConfigurations = new List<IMappingConfiguration>();
        }
        
        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression)
        {
            CreateMapping(mappingExpression, dependencyResolver, new FlashMapperMappingCallbacks<TSource, TDestination>(null, null));
        }

        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperMappingConfigurator<TSource, TDestination>, IFlashMapperMappingConfigurator<TSource, TDestination>> settings)
        {
            var configurator = new FlashMapperMappingConfigurator<TSource, TDestination>(dependencyResolver);
            settings(configurator);
            var customSettings = configurator.GetSettings();
            var resolver = configurator.GetResultDependencyResolver();
            var defaultFlashMapperSettings = resolver.GetService<IFlashMapperSettings>();
            var mappingSettings = resolver.GetService<IFlashMapperSettingsExtender>()
                .Extend(defaultFlashMapperSettings, customSettings);
            configurator.RegisterService(r => mappingSettings);
            CreateMapping(mappingExpression, resolver, configurator.GetMappingCallbacks());
        }
        
        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings, 
            Func<IFlashMapperCustomServiceBuilder, IFlashMapperCustomServiceBuilder> customServicesRegistration)
        {
            var flashMapperCustomServiceBuilderFactory = dependencyResolver
                .GetService<IFlashMapperCustomServiceBuilderFactory>();
            var customSettings = ResolveCustomSettings(settings, dependencyResolver);
            var flashMapperCustomServiceBuilder = flashMapperCustomServiceBuilderFactory.Create();
            var resolver = customServicesRegistration(flashMapperCustomServiceBuilder)
                .RegisterService(r => customSettings)
                .GetResultDependencyResolver();
            CreateMapping(mappingExpression, resolver, new FlashMapperMappingCallbacks<TSource, TDestination>(null, null));
        }

        private IFlashMapperSettings ResolveCustomSettings(Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings,
            IFlashMapperDependencyResolver resolver)
        {
            var settingsBuilder = resolver.GetService<IFlashMapperSettingsBuilderFactory>().GetBuilder();
            var customSettings = settings(settingsBuilder).GetSettings();
            var defaultFlashMapperSettings = resolver.GetService<IFlashMapperSettings>();
            var mappingSettings = resolver.GetService<IFlashMapperSettingsExtender>()
                .Extend(defaultFlashMapperSettings, customSettings);
            return mappingSettings;
        }
        
        private void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, IFlashMapperDependencyResolver resolver, IFlashMapperMappingCallbacks<TSource, TDestination> callbacks)
        {
            var mappingGenerator = resolver.GetService<IMappingGenerator>();
            var mapping = mappingGenerator.GenerateCompleteMapping(mappingExpression, callbacks);
            mappingsStorage.SetMapping(mapping);
        }

        public void MapData<TSource, TDestination>(TSource source, TDestination destination)
        {
            var mapping = mappingsStorage.GetMapping<TSource, TDestination>();
            mapping.MapDataFunction(source, destination);
        }

        public TDestination Convert<TSource, TDestination>(TSource source)
        {
            var mapping = mappingsStorage.GetMapping<TSource, TDestination>();
            return mapping.BuildFunction(source);
        }
        
        public IMappingConfiguration CreateDependantConfiguration()
        {
            var configuration = new MappingConfiguration(customServices);
            dependantConfigurations.Add(configuration);
            return configuration;
        }

        public Guid InstanceId { get; }

        public void Dispose()
        {
            mappingsStorage?.Dispose();
            foreach (var dependantConfiguration in dependantConfigurations)
                dependantConfiguration?.Dispose();
        }
    }

}