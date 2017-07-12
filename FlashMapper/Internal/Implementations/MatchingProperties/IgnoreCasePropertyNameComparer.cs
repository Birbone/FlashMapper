using System;
using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class IgnoreCasePropertyNameComparer : IIgnoreCasePropertyNameComparer
    {
        public PropertyNameCompareRank Compare(string sourceName, string destinationName, IFlashMapperSettings modelMapperSettings)
        {
            var sourceNamingConvention = modelMapperSettings.NamingConventions.Source.NamingConventionType;
            var destinationNamingConvention = modelMapperSettings.NamingConventions.Destination.NamingConventionType;

            if (!NamingConventionType.CaseInsensitive.In(sourceNamingConvention, destinationNamingConvention))
                return PropertyNameCompareRank.DoNotMatch;

            if (sourceName == destinationName)
                return PropertyNameCompareRank.ExactMatch;

            if (sourceName.Equals(destinationName, StringComparison.InvariantCultureIgnoreCase))
                return PropertyNameCompareRank.IgnoreCaseMatch;

            return PropertyNameCompareRank.DoNotMatch;
        }
    }
}