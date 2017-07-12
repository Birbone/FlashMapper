namespace FlashMapper.Services.Settings
{
    public interface IFlashMapperSettingsBuilderFactory: IFlashMapperService
    {
        IFlashMapperSettingsBuilder GetBuilder();
    }
}