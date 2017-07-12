using FlashMapper.Models;

namespace FlashMapper
{
    public interface IFlashMapperSettingsBuilder
    {
        IFlashMapperSettingsBuilder UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior);
        IFlashMapperSettingsBuilder CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior);
        IFlashMapperSettingsBuilder SourceNamingConvention(INamingConvention namingConvention);
        IFlashMapperSettingsBuilder SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
        IFlashMapperSettingsBuilder DestinationNamingConvention(INamingConvention namingConvention);
        IFlashMapperSettingsBuilder DestinationNamingConvention(NamingConventionType namingConventionType, params string[] prefixes);
        IFlashMapperSettings GetSettings();
    }
}