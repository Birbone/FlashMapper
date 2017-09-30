using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class NamingUnspecifiedPropertyNameComparer : INamingUnspecifiedPropertyNameComparer
    {
        private readonly IFlashMapperSettings settings;

        public NamingUnspecifiedPropertyNameComparer(IFlashMapperSettings settings)
        {
            this.settings = settings;
        }

        public PropertyNameCompareRank Compare(string sourceName, string destinationName)
        {
            var sourceNamingConvention = settings.NamingConventions.Source.NamingConventionType;
            var destinationNamingConvention = settings.NamingConventions.Destination.NamingConventionType;

            if (!NamingConventionType.Unspecified.In(sourceNamingConvention, destinationNamingConvention))
                return PropertyNameCompareRank.DoNotMatch;

            return sourceName == destinationName
                ? PropertyNameCompareRank.ExactMatch
                : PropertyNameCompareRank.DoNotMatch;
        }
    }
}