using FlashMapper.Models;

namespace FlashMapper
{
    public interface IFlashMapperSettingsBuilder
    {
        /// <summary>
        /// Allows you to specify to FlashMapper what it needs to do when some property in destination model cannot be mapped from source
        /// </summary>
        /// <param name="unresolvedPropertyBehavior"></param>
        /// <returns>Self</returns>
        IFlashMapperSettingsBuilder UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior);
        /// <summary>
        /// Allows you to specify to FlashMapper what it needs to do when some property in destination model can be mapped from few properties in source model
        /// </summary>
        /// <param name="selectSourceCollisionBehavior"></param>
        /// <returns>Self</returns>
        IFlashMapperSettingsBuilder CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior);
        /// <summary>
        /// Allows you to specify <see cref="INamingConvention"/> of source model
        /// </summary>
        /// <param name="namingConvention">Naming convention that describes names of properties of the source model</param>
        /// <returns>Self</returns>
        IFlashMapperSettingsBuilder SourceNamingConvention(INamingConvention namingConvention);
        /// <summary>
        /// Allows you to specify <see cref="NamingConventionType"/> and possible prefixes of properties of source model
        /// </summary>
        /// <param name="namingConventionType">Naming convention that describes names of properties of the source model</param>
        /// <param name="prefixes">Some prefixes like "i_" or "s_" that properties of source model could have</param>
        /// <returns>Self</returns>
        IFlashMapperSettingsBuilder SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
        /// <summary>
        /// Allows you to specify <see cref="INamingConvention"/> of destination model
        /// </summary>
        /// <param name="namingConvention">Naming convention that describes names of properties of the destination model</param>
        /// <returns>Self</returns>
        IFlashMapperSettingsBuilder DestinationNamingConvention(INamingConvention namingConvention);
        /// <summary>
        /// Allows you to specify <see cref="NamingConventionType"/> and possible prefixes of properties of destination model
        /// </summary>
        /// <param name="namingConventionType">Naming convention that describes names of properties of the destination model</param>
        /// <param name="prefixes">Some prefixes like "i_" or "s_" that properties of destination model could have</param>
        /// <returns></returns>
        IFlashMapperSettingsBuilder DestinationNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
        /// <summary>
        /// Method that returns all settings that have been specified earlier
        /// Used internaly
        /// </summary>
        /// <returns>All configured settings</returns>
        IFlashMapperSettings GetSettings();
    }

    public interface IFlashMapperSettingsBuilder<out TConfigurator> where TConfigurator: IFlashMapperSettingsBuilder<TConfigurator>
    {
        /// <summary>
        /// Allows you to specify to FlashMapper what it needs to do when some property in destination model cannot be mapped from source
        /// </summary>
        /// <param name="unresolvedPropertyBehavior"></param>
        /// <returns>Self</returns>
        TConfigurator UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior);
        /// <summary>
        /// Allows you to specify to FlashMapper what it needs to do when some property in destination model can be mapped from few properties in source model
        /// </summary>
        /// <param name="selectSourceCollisionBehavior"></param>
        /// <returns>Self</returns>
        TConfigurator CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior);
        /// <summary>
        /// Allows you to specify <see cref="INamingConvention"/> of source model
        /// </summary>
        /// <param name="namingConvention">Naming convention that describes names of properties of the source model</param>
        /// <returns>Self</returns>
        TConfigurator SourceNamingConvention(INamingConvention namingConvention);
        /// <summary>
        /// Allows you to specify <see cref="NamingConventionType"/> and possible prefixes of properties of source model
        /// </summary>
        /// <param name="namingConventionType">Naming convention that describes names of properties of the source model</param>
        /// <param name="prefixes">Some prefixes like "i_" or "s_" that properties of source model could have</param>
        /// <returns>Self</returns>
        TConfigurator SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
        /// <summary>
        /// Allows you to specify <see cref="INamingConvention"/> of destination model
        /// </summary>
        /// <param name="namingConvention">Naming convention that describes names of properties of the destination model</param>
        /// <returns>Self</returns>
        TConfigurator DestinationNamingConvention(INamingConvention namingConvention);
        /// <summary>
        /// Allows you to specify <see cref="NamingConventionType"/> and possible prefixes of properties of destination model
        /// </summary>
        /// <param name="namingConventionType">Naming convention that describes names of properties of the destination model</param>
        /// <param name="prefixes">Some prefixes like "i_" or "s_" that properties of destination model could have</param>
        /// <returns></returns>
        TConfigurator DestinationNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
    }
}