using System.Collections.Generic;

namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameTokenizer
    {
        IEnumerable<string> GetTokens(string propertyName);
    }
}