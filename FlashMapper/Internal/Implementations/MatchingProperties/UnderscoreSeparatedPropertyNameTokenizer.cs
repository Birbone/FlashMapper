using System.Collections.Generic;
using System.Linq;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class UnderscoreSeparatedPropertyNameTokenizer : IPropertyNameTokenizer
    {
        public IEnumerable<string> GetTokens(string propertyName)
        {
            return propertyName.Split('_')
                .Where(t => !string.IsNullOrEmpty(t));
        }
    }
}