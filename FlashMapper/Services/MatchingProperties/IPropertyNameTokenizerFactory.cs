namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameTokenizerFactory
    {
        bool TryCreate(NamingConventionType namingConvention, out IPropertyNameTokenizer propertyNameTokenizer);
    }
}