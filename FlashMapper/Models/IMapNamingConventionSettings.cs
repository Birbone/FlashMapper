namespace FlashMapper.Models
{
    public interface IMapNamingConventionSettings
    {
        INamingConvention Source { get; }
        INamingConvention Destination { get; }
    }

    public class DefaultMapNamingConventionSettings : IMapNamingConventionSettings
    {
        public DefaultNamingConvention Source { get; set; }
        INamingConvention IMapNamingConventionSettings.Source => Source;
        public DefaultNamingConvention Destination { get; set; }
        INamingConvention IMapNamingConventionSettings.Destination => Destination;
    }
}