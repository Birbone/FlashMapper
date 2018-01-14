using System;
using FlashMapper.Services.Settings;

namespace FlashMapper.Internal.Implementations.Settings
{
    [Obsolete]
    public class FlashMapperSettingsBuilderFactory : IFlashMapperSettingsBuilderFactory
    {
        public IFlashMapperSettingsBuilder GetBuilder()
        {
            return new FlashMapperSettingsBuilder();
        }
    }
}