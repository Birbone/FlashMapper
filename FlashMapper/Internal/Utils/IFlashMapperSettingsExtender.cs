using FlashMapper.Models;

namespace FlashMapper.Internal.Utils
{
    public interface IFlashMapperSettingsExtender
    {
        IFlashMapperSettings Extend(IFlashMapperSettings defaultSettings, IFlashMapperSettings customSettings);
    }
}