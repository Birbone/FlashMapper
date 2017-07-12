namespace FlashMapper.Models
{
    public class FlashMapperSettings: IFlashMapperSettings
    {
        public FlashMapperSettings(UnresolvedPropertyBehavior unresolvedBehavior,
            SelectSourceCollisionBehavior collisionBehavior, 
            IMapNamingConventionSettings namingConventions)
        {
            UnresolvedBehavior = unresolvedBehavior;
            CollisionBehavior = collisionBehavior;
            NamingConventions = namingConventions;
        }

        public UnresolvedPropertyBehavior UnresolvedBehavior { get; }
        public SelectSourceCollisionBehavior CollisionBehavior { get; }
        public IMapNamingConventionSettings NamingConventions { get; }
    }
}