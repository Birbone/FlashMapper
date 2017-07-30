using FlashMapper.Models;

namespace FlashMapper.Services.Settings
{
    /// <summary>
    /// Service that returns default settings for all mappings
    /// </summary>
    public interface IDefaultFlashMapperSettingsProvider: IFlashMapperService
    {
        /// <summary>
        /// Returns default settings for <see cref="MappingConfiguration"/>
        /// </summary>
        /// <returns>Default settings</returns>
        IFlashMapperSettings GetDefaultSettings();
    }
}