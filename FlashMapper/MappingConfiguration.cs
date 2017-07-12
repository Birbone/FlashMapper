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
    public class MappingConfiguration : IMappingConfiguration
    {
        private readonly IFlashMapperDependencyResolver dependencyResolver;
        private readonly IMappingsStorage mappingsStorage;
        private readonly IFlashMapperSettings defaultFlashMapperSettings;

        public MappingConfiguration(): this(Enumerable.Empty<IFlashMapperService>())
        {
            
        }

        public MappingConfiguration(IEnumerable<IFlashMapperService> customServices)
        {
            InstanceId = Guid.NewGuid();
            var customServicesArray = customServices.ToArray();

            dependencyResolver = customServicesArray.Length > 0
                ? new FlashMapperDependencyResolver(ModuleConfiguration.GetDefaultResolver(), customServicesArray)
                : ModuleConfiguration.GetDefaultResolver();

            mappingsStorage = dependencyResolver.GetService<IMappingsStorageFactory>().Create(this);
            defaultFlashMapperSettings = dependencyResolver.GetService<IDefaultFlashMapperSettingsProvider>().GetDefaultSettings();
        }


        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression)
        {
            CreateMapping(mappingExpression, defaultFlashMapperSettings, dependencyResolver);
        }

        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings)
        {
            CreateMapping(mappingExpression, ResolveCustomSettings(settings, dependencyResolver), dependencyResolver);
        }
        
        public void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings, Func<IFlashMapperCustomServiceBuilder, IFlashMapperCustomServiceBuilder> customServicesRegistration)
        {
            var modelMapperCustomServiceBuilder = dependencyResolver
                .GetService<IFlashMapperCustomServiceBuilderFactory>()
                .Create();
            var resolver = customServicesRegistration(modelMapperCustomServiceBuilder).GetResultDependencyResolver();
            var mappingSettings = ResolveCustomSettings(settings, resolver);
            CreateMapping(mappingExpression, mappingSettings, resolver);
        }

        private IFlashMapperSettings ResolveCustomSettings(Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings,
            IFlashMapperDependencyResolver resolver)
        {
            var settingsBuilder = resolver.GetService<IFlashMapperSettingsBuilderFactory>().GetBuilder();
            var customSettings = settings(settingsBuilder).GetSettings();
            var mappingSettings = resolver.GetService<IFlashMapperSettingsExtender>()
                .Extend(defaultFlashMapperSettings, customSettings);
            return mappingSettings;
        }
        
        private void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, IFlashMapperSettings settings, IFlashMapperDependencyResolver resolver)
        {
            var mapping = resolver.GetService<IMappingGenerator>().GenerateCompleteMapping(mappingExpression, settings);
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

        public Guid InstanceId { get; }
    }

}