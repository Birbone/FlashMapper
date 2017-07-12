using FlashMapper.Models;

namespace FlashMapper.Internal.Utils
{
    public class FlashMapperSettingsExtender : IFlashMapperSettingsExtender
    {
        public IFlashMapperSettings Extend(IFlashMapperSettings defaultSettings, IFlashMapperSettings customSettings)
        {
            var resultUnresolvedBehavior = customSettings.UnresolvedBehavior == UnresolvedPropertyBehavior.Inherit
                ? defaultSettings.UnresolvedBehavior
                : customSettings.UnresolvedBehavior;
            var resultCollisionBehavior = customSettings.CollisionBehavior == SelectSourceCollisionBehavior.Inherit
                ? defaultSettings.CollisionBehavior
                : customSettings.CollisionBehavior;
            var resultSourceNamingConvension = customSettings.NamingConventions.Source ?? defaultSettings.NamingConventions.Source;
            var resultDestinationNamingConvension = customSettings.NamingConventions.Destination ?? defaultSettings.NamingConventions.Destination;

            return new FlashMapperSettings(resultUnresolvedBehavior, resultCollisionBehavior,
                new MapNamingConventionSettings(resultSourceNamingConvension, resultDestinationNamingConvension));
        }
    }
}