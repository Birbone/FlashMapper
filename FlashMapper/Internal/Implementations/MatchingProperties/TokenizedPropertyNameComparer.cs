using System;
using System.Linq;
using FlashMapper.Models;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class TokenizedPropertyNameComparer : ITokenizedPropertyNameComparer
    {
        private readonly IPropertyNameTokenizerResolver tokenizerResolver;
        private readonly IPropertyNameAbbrevationHandler propertyNameAbbrevationHandler;
        private readonly IFlashMapperSettings settings;

        public TokenizedPropertyNameComparer(IPropertyNameTokenizerResolver tokenizerResolver, 
            IPropertyNameAbbrevationHandler propertyNameAbbrevationHandler,
            IFlashMapperSettings settings)
        {
            this.tokenizerResolver = tokenizerResolver;
            this.propertyNameAbbrevationHandler = propertyNameAbbrevationHandler;
            this.settings = settings;
        }

        private bool TryGetTokens(string propertyName, NamingConventionType namingConvention, out string[] tokens)
        {
            IPropertyNameTokenizer tokenizer;
            if (!tokenizerResolver.TryGetTokenizer(namingConvention, out tokenizer))
            {
                tokens = null;
                return false;
            }
            tokens = propertyNameAbbrevationHandler.HandleAbbrevations(tokenizer.GetTokens(propertyName)).ToArray();
            return true;
        }

        public PropertyNameCompareRank Compare(string sourceName, string destinationName)
        {
            string[] sourceTokens;
            string[] destinationTokens;
            if (!TryGetTokens(sourceName, settings.NamingConventions.Source.NamingConventionType,
                out sourceTokens))
            {
                return PropertyNameCompareRank.DoNotMatch;
            }
            if (!TryGetTokens(destinationName, settings.NamingConventions.Destination.NamingConventionType,
                out destinationTokens))
            {
                return PropertyNameCompareRank.DoNotMatch;
            }
            if (sourceTokens.Length != destinationTokens.Length)
                return PropertyNameCompareRank.DoNotMatch;
            var compareResult = sourceTokens.Zip(destinationTokens, (s, d) => new
            {
                Source = s,
                Destination = d
            }).All(tp => tp.Source.Equals(tp.Destination, StringComparison.InvariantCultureIgnoreCase));

            return compareResult 
                ? PropertyNameCompareRank.UserSpecifiedMatch 
                : PropertyNameCompareRank.DoNotMatch;
        }
    }
}