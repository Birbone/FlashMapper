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

        public TokenizedPropertyNameComparer(IPropertyNameTokenizerResolver tokenizerResolver, IPropertyNameAbbrevationHandler propertyNameAbbrevationHandler)
        {
            this.tokenizerResolver = tokenizerResolver;
            this.propertyNameAbbrevationHandler = propertyNameAbbrevationHandler;
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

        public PropertyNameCompareRank Compare(string sourceName, string destinationName, IFlashMapperSettings modelMapperSettings)
        {
            string[] sourceTokens;
            string[] destinationTokens;
            if (!TryGetTokens(sourceName, modelMapperSettings.NamingConventions.Source.NamingConventionType,
                out sourceTokens))
            {
                return PropertyNameCompareRank.DoNotMatch;
            }
            if (!TryGetTokens(destinationName, modelMapperSettings.NamingConventions.Destination.NamingConventionType,
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