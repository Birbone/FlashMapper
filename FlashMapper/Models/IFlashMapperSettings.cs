namespace FlashMapper.Models
{
    public interface IFlashMapperSettings
    {
        UnresolvedPropertyBehavior UnresolvedBehavior { get; }
        SelectSourceCollisionBehavior CollisionBehavior { get; }
        IMapNamingConventionSettings NamingConventions { get; }
    }
}