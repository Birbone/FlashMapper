using FlashMapper.Models;

namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameComparer : IFlashMapperService
    {
        PropertyNameCompareRank Compare(string searchPropertyName, string optionPropertyName);
    }
}