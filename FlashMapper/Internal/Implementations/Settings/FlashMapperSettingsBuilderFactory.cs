using FlashMapper.Services.Settings;

namespace FlashMapper.Internal.Implementations.Settings
{
    public class FlashMapperSettingsBuilderFactory : IFlashMapperSettingsBuilderFactory
    {
        public IFlashMapperSettingsBuilder GetBuilder()
        {
            return new FlashMapperSettingsBuilder();
        }
    }
}