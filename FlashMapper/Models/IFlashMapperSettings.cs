using FlashMapper.Services;

namespace FlashMapper.Models
{
    public interface IFlashMapperSettings: IFlashMapperService
    {
        UnresolvedPropertyBehavior UnresolvedBehavior { get; }
        SelectSourceCollisionBehavior CollisionBehavior { get; }
        IMapNamingConventionSettings NamingConventions { get; }
    }
}