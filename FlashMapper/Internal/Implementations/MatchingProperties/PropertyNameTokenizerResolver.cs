using System.Collections.Generic;
using System.Linq;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class PropertyNameTokenizerResolver : IPropertyNameTokenizerResolver
    {
        private readonly IEnumerable<IPropertyNameTokenizerFactory> factories;

        public PropertyNameTokenizerResolver(IEnumerable<IPropertyNameTokenizerFactory> factories)
        {
            this.factories = factories.ToArray();
        }

        public bool TryGetTokenizer(NamingConventionType namingConvention, out IPropertyNameTokenizer propertyNameTokenizer)
        {
            IPropertyNameTokenizer result = null;
            if (factories.Any(f => f.TryCreate(namingConvention, out result)))
            {
                propertyNameTokenizer = result;
                return true;
            }

            propertyNameTokenizer = null;
            return false;
        }
    }
}