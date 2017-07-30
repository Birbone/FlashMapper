namespace FlashMapper.Services.Settings
{
    /// <summary>
    /// Service that creates new instance of <see cref="IFlashMapperSettingsBuilder"/> to configure mapping settings
    /// </summary>
    public interface IFlashMapperSettingsBuilderFactory: IFlashMapperService
    {
        /// <summary>
        /// Creates new instance of <see cref="IFlashMapperSettingsBuilder"/>
        /// </summary>
        /// <returns></returns>
        IFlashMapperSettingsBuilder GetBuilder();
    }
}