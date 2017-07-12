using FlashMapper.Models;

namespace FlashMapper.Services.MatchingProperties
{
    public interface ISpecificPropertyNameComparer
    {
        PropertyNameCompareRank Compare(string sourceName, string destinationName, IFlashMapperSettings modelMapperSettings);
    }
}