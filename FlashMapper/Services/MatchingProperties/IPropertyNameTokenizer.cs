using System.Collections.Generic;

namespace FlashMapper.Services.MatchingProperties
{
    /// <summary>
    /// Service that splits name of the propery on its tokens
    /// </summary>
    public interface IPropertyNameTokenizer
    {
        /// <summary>
        /// Splits <see cref="propertyName"/> on tokens.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IEnumerable<string> GetTokens(string propertyName);
    }
}