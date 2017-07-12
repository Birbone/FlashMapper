namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameTokenizerResolver : IFlashMapperService
    {
        bool TryGetTokenizer(NamingConventionType namingConvention, out IPropertyNameTokenizer propertyNameTokenizer);
    }
}