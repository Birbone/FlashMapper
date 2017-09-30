using System.Collections.Generic;
using System.Linq;
using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class PropertyNameComparer : IPropertyNameComparer
    {
        private readonly IPropertyPrefixLocator propertyPrefixLocator;
        private readonly IFlashMapperSettings settings;
        private readonly IEnumerable<ISpecificPropertyNameComparer> specificPropertyNameComparers;

        public PropertyNameComparer(IPropertyPrefixLocator propertyPrefixLocator,
            IEnumerable<ISpecificPropertyNameComparer> specificPropertyNameComparers,
            IFlashMapperSettings settings)
        {
            this.propertyPrefixLocator = propertyPrefixLocator;
            this.settings = settings;
            this.specificPropertyNameComparers = specificPropertyNameComparers.ToArray();
        }

        private IEnumerable<string> GetNameOptionsWithoutPrefix(string propertyName, string[] prefixes)
        {
            foreach (var prefix in prefixes)
            {
                string result;
                if (propertyPrefixLocator.TryRemovePrefix(propertyName, prefix, out result))
                    yield return result;
            }
        }

        public PropertyNameCompareRank Compare(string searchPropertyName, string optionPropertyName)
        {
            var sourcePrefixes = settings.NamingConventions
                .Source
                .Prefixes
                .With("")
                .Distinct()
                .ToArray();
            var destinationPrefixes = settings.NamingConventions
                .Destination
                .Prefixes
                .With("")
                .Distinct()
                .ToArray();
            var sourceNameOptions = GetNameOptionsWithoutPrefix(optionPropertyName, sourcePrefixes);
            var destinationNameOptions = GetNameOptionsWithoutPrefix(searchPropertyName, destinationPrefixes);
            var nameCombinations = sourceNameOptions.GetAllCombinations(destinationNameOptions);
            var rank = nameCombinations.SelectMany(nc => specificPropertyNameComparers.Select(c => c.Compare(nc.Source, nc.Destination)))
                .Max();
            return rank;
        }
    }
}