using FlashMapper.Models;
using FlashMapper.Services.Settings;

namespace FlashMapper.Internal.Implementations.Settings
{
    public class DefaultFlashMapperSettingsProvider : IDefaultFlashMapperSettingsProvider
    {
        private readonly IFlashMapperSettingsBuilderFactory modelMapperSettingsBuilderFactory;
        public DefaultFlashMapperSettingsProvider(IFlashMapperSettingsBuilderFactory modelMapperSettingsBuilderFactory)
        {
            this.modelMapperSettingsBuilderFactory = modelMapperSettingsBuilderFactory;
        }

        public IFlashMapperSettings GetDefaultSettings()
        {
            return modelMapperSettingsBuilderFactory.GetBuilder()
                .UnresolvedBehavior(UnresolvedPropertyBehavior.Fail)
                .CollisionBehavior(SelectSourceCollisionBehavior.Fail)
                .SourceNamingConvention(NamingConventionType.Unspecified)
                .DestinationNamingConvention(NamingConventionType.Unspecified)
                .GetSettings();
        }
    }
}