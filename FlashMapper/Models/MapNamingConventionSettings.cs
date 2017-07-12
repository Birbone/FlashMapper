namespace FlashMapper.Models
{
    public class MapNamingConventionSettings : IMapNamingConventionSettings
    {
        public MapNamingConventionSettings(INamingConvention sourceNamingConvention, INamingConvention destinationNamingConvention)
        {
            Source = sourceNamingConvention;
            Destination = destinationNamingConvention;
        }

        public INamingConvention Source { get; }
        public INamingConvention Destination { get; }
    }
}