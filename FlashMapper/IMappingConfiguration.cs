using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlashMapper
{
    public interface IMappingConfiguration
    {
        void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression);
        void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings);
        void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings, 
            Func<IFlashMapperCustomServiceBuilder, IFlashMapperCustomServiceBuilder> customServicesRegistration);
        void MapData<TSource, TDestination>(TSource source, TDestination destination);
        TDestination Convert<TSource, TDestination>(TSource source);
    }
}