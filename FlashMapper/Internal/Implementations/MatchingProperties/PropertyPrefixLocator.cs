using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.MatchingProperties
{
    public class PropertyPrefixLocator : IPropertyPrefixLocator
    {
        public bool TryRemovePrefix(string propertyName, string prefix, out string propertyWithoutPrefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                propertyWithoutPrefix = propertyName;
                return true;
            }
            if (propertyName.StartsWith(prefix))
            {
                propertyWithoutPrefix = propertyName.Substring(prefix.Length);
                return true;
            }
            propertyWithoutPrefix = null;
            return false;
        }
    }
}