using System;
using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper
{
    [Obsolete]
    internal class SimplePropertyNameComparer : IPropertyNameComparer
    {
        public PropertyNameCompareRank Compare(string searchPropertyName, string optionPropertyName)
        {
            return searchPropertyName == optionPropertyName ? PropertyNameCompareRank.ExactMatch : PropertyNameCompareRank.DoNotMatch;
        }
    }
}