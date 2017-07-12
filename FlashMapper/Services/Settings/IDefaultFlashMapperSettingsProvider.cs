using FlashMapper.Models;

namespace FlashMapper.Services.Settings
{
    public interface IDefaultFlashMapperSettingsProvider: IFlashMapperService
    {
        IFlashMapperSettings GetDefaultSettings();
    }
}