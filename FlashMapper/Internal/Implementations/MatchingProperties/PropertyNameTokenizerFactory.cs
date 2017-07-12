using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public abstract class PropertyNameTokenizerFactory : IPropertyNameTokenizerFactory
    {
        private readonly NamingConventionType namingConvention;

        protected PropertyNameTokenizerFactory(NamingConventionType namingConvention)
        {
            this.namingConvention = namingConvention;
        }

        protected abstract IPropertyNameTokenizer CreateTokenizer(NamingConventionType namingConvention);

        public bool TryCreate(NamingConventionType namingConvention, out IPropertyNameTokenizer propertyNameTokenizer)
        {
            if (this.namingConvention != namingConvention)
            {
                propertyNameTokenizer = null;
                return false;
            }
            propertyNameTokenizer = CreateTokenizer(namingConvention);
            return true;
        }
    }
}