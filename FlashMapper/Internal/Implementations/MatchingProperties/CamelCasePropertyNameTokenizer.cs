using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class CamelCasePropertyNameTokenizer : IPropertyNameTokenizer
    {
        public IEnumerable<string> GetTokens(string propertyName)
        {
            return Regex.Split(propertyName, @"(?<!^)(?=[A-Z])");
        }
    }
}