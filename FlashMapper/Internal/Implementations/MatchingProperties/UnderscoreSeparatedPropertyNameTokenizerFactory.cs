using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class UnderscoreSeparatedPropertyNameTokenizerFactory : PropertyNameTokenizerFactory, IUnderscoreSeparatedPropertyNameTokenizerFactory
    {
        public UnderscoreSeparatedPropertyNameTokenizerFactory() : base(NamingConventionType.UnderscoreSeparated)
        {
        }

        protected override IPropertyNameTokenizer CreateTokenizer(NamingConventionType namingConvention)
        {
            return new UnderscoreSeparatedPropertyNameTokenizer();
        }
    }
}