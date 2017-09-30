using System;
using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class IgnoreCasePropertyNameComparer : IIgnoreCasePropertyNameComparer
    {
        private readonly IFlashMapperSettings settings;

        public IgnoreCasePropertyNameComparer(IFlashMapperSettings settings)
        {
            this.settings = settings;
        }

        public PropertyNameCompareRank Compare(string sourceName, string destinationName)
        {
            var sourceNamingConvention = settings.NamingConventions.Source.NamingConventionType;
            var destinationNamingConvention = settings.NamingConventions.Destination.NamingConventionType;

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