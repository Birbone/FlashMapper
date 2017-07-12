using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class NamingUnspecifiedPropertyNameComparer : INamingUnspecifiedPropertyNameComparer
    {
        public PropertyNameCompareRank Compare(string sourceName, string destinationName, IFlashMapperSettings modelMapperSettings)
        {
            var sourceNamingConvention = modelMapperSettings.NamingConventions.Source.NamingConventionType;
            var destinationNamingConvention = modelMapperSettings.NamingConventions.Destination.NamingConventionType;

            if (!NamingConventionType.Unspecified.In(sourceNamingConvention, destinationNamingConvention))
                return PropertyNameCompareRank.DoNotMatch;

            return sourceName == destinationName
                ? PropertyNameCompareRank.ExactMatch
                : PropertyNameCompareRank.DoNotMatch;
        }
    }
}