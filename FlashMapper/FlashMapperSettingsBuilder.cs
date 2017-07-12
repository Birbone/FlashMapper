using FlashMapper.Models;

namespace FlashMapper
{
    public class FlashMapperSettingsBuilder : IFlashMapperSettingsBuilder
    {
        private UnresolvedPropertyBehavior unresolvedPropertyBehavior;
        private SelectSourceCollisionBehavior selectSourceCollisionBehavior;
        private INamingConvention sourceNamingConvention;
        private INamingConvention destinationNamingConvention;

        public FlashMapperSettingsBuilder()
        {
            unresolvedPropertyBehavior = UnresolvedPropertyBehavior.Inherit;
            selectSourceCollisionBehavior = SelectSourceCollisionBehavior.Inherit;
            sourceNamingConvention = null;
            destinationNamingConvention = null;
        }

        public IFlashMapperSettingsBuilder UnresolvedBehavior(UnresolvedPropertyBehavior unresolvedPropertyBehavior)
        {
            this.unresolvedPropertyBehavior = unresolvedPropertyBehavior;
            return this;
        }

        public IFlashMapperSettingsBuilder CollisionBehavior(SelectSourceCollisionBehavior selectSourceCollisionBehavior)
        {
            this.selectSourceCollisionBehavior = selectSourceCollisionBehavior;
            return this;
        }

        public IFlashMapperSettingsBuilder SourceNamingConvention(INamingConvention namingConvention)
        {
            this.sourceNamingConvention = namingConvention;
            return this;
        }

        public IFlashMapperSettingsBuilder SourceNamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            return SourceNamingConvention(new NamingConvention(namingConventionType, prefixes));
        }

        public IFlashMapperSettingsBuilder DestinationNamingConvention(INamingConvention namingConvention)
        {
            this.destinationNamingConvention = namingConvention;
            return this;
        }

        public IFlashMapperSettingsBuilder DestinationNamingConvention(NamingConventionType namingConventionType,
            params string[] prefixes)
        {
            return DestinationNamingConvention(new NamingConvention(namingConventionType, prefixes));
        }

        public IFlashMapperSettings GetSettings()
        {
            return new FlashMapperSettings(unresolvedPropertyBehavior, selectSourceCollisionBehavior,
                new MapNamingConventionSettings(sourceNamingConvention, destinationNamingConvention));
        }
    }
}