namespace FlashMapper.Models
{
    public interface IMapNamingConventionSettings
    {
        INamingConvention Source { get; }
        INamingConvention Destination { get; }
    }
}