using System.Collections.Generic;
using System.Text;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class PropertyNameAbbrevationHandler : IPropertyNameAbbrevationHandler
    {
        public IEnumerable<string> HandleAbbrevations(IEnumerable<string> tokens)
        {
            var abbrevationToken = new StringBuilder();
            foreach (var token in tokens)
            {
                if (token.Length == 0)
                    continue;
                if (token.Length == 1)
                {
                    abbrevationToken.Append(token);
                    continue;
                }
                if (abbrevationToken.Length > 0)
                {
                    yield return abbrevationToken.ToString();
                    abbrevationToken = new StringBuilder();
                }
                yield return token;
            }

            if (abbrevationToken.Length > 0)
                yield return abbrevationToken.ToString();
        }
    }
}