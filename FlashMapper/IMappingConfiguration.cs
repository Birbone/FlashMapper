using System;
using System.Linq.Expressions;

namespace FlashMapper
{
    /// <summary>
    /// Service that lets you configure mappings and then use them to convert models, or map data from one to another
    /// </summary>
    public interface IMappingConfiguration: IDisposable
    {
        /// <summary>
        /// Configures and stores mapping for later use
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="mappingExpression">Expression that describes conversion from source model to destination</param>
        IMappingConfiguration CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression);
        /// <summary>
        /// Configures and stores mapping for later use
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="mappingExpression">Expression that describes conversion from source model to destination</param>
        /// <param name="settings">Settings configurator</param>
        IMappingConfiguration CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperMappingConfigurator<TSource, TDestination>, IFlashMapperMappingConfigurator<TSource, TDestination>> settings);
        /// <summary>
        /// Configures and stores mapping for later use
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="mappingExpression">Expression that describes conversion from source model to destination</param>
        /// <param name="settings">Settings configurator</param>
        /// <param name="customServicesRegistration">Configurator that allows you to override any internal service</param>
        [Obsolete("Register custom services in settings.")]
        void CreateMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> mappingExpression, 
            Func<IFlashMapperSettingsBuilder, IFlashMapperSettingsBuilder> settings, 
            Func<IFlashMapperCustomServiceBuilder, IFlashMapperCustomServiceBuilder> customServicesRegistration);
        /// <summary>
        /// Maps data from source model to destination via mapping defined earlier
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="source">Original model</param>
        /// <param name="destination">Result model</param>
        void MapData<TSource, TDestination>(TSource source, TDestination destination);
        /// <summary>
        /// Converts source model into destination with mapping defined earlier
        /// </summary>
        /// <typeparam name="TSource">Original model</typeparam>
        /// <typeparam name="TDestination">Result model</typeparam>
        /// <param name="source">Original model</param>
        /// <returns>Result model</returns>
        TDestination Convert<TSource, TDestination>(TSource source);
        /// <summary>
        /// Creates another configuration that will be disposed with this configuration
        /// </summary>
        /// <returns></returns>
        IMappingConfiguration CreateDependantConfiguration();
    }
}