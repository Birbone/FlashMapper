using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class CamelCasePropertyNameTokenizerFactory : PropertyNameTokenizerFactory, ICamelCasePropertyNameTokenizerFactory
    {
        public CamelCasePropertyNameTokenizerFactory() : base(NamingConventionType.CamelCase)
        {
        }

        protected override IPropertyNameTokenizer CreateTokenizer(NamingConventionType namingConvention)
        {
            return new CamelCasePropertyNameTokenizer();
        }
    }
}