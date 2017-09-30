namespace FlashMapper.Models
{
    public class DefaultFlashMapperSettings : IFlashMapperSettings
    {
        public DefaultFlashMapperSettings()
        {
            UnresolvedBehavior = UnresolvedPropertyBehavior.Fail;
            CollisionBehavior = SelectSourceCollisionBehavior.Fail;
            NamingConventions = new DefaultMapNamingConventionSettings
            {
                Source = new DefaultNamingConvention { NamingConventionType = NamingConventionType.Unspecified },
                Destination = new DefaultNamingConvention { NamingConventionType = NamingConventionType.Unspecified }
            };
        }

        public UnresolvedPropertyBehavior UnresolvedBehavior { get; set; }
        public SelectSourceCollisionBehavior CollisionBehavior { get; set; }
        public DefaultMapNamingConventionSettings NamingConventions { get; set; }
        IMapNamingConventionSettings IFlashMapperSettings.NamingConventions => NamingConventions;
    }
}