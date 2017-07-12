namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyPrefixLocator: IFlashMapperService
    {
        bool TryRemovePrefix(string propertyName, string prefix, out string propertyWithoutPrefix);
    }
}