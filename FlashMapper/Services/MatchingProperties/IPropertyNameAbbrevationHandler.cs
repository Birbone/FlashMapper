using System.Collections.Generic;

namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameAbbrevationHandler : IFlashMapperService
    {
        IEnumerable<string> HandleAbbrevations(IEnumerable<string> tokens);
    }
}