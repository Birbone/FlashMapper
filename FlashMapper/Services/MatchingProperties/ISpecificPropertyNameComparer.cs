using FlashMapper.Models;

namespace FlashMapper.Services.MatchingProperties
{
    /// <summary>
    /// Service that checks if names of properties in source and destination models are similar
    /// </summary>
    public interface ISpecificPropertyNameComparer
    {
        /// <summary>
        /// Returns similarity of two property names
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        PropertyNameCompareRank Compare(string sourceName, string destinationName);
    }
}