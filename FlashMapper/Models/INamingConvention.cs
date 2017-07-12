using System.Collections.Generic;

namespace FlashMapper.Models
{
    public interface INamingConvention
    {
        NamingConventionType NamingConventionType { get; }
        IEnumerable<string> Prefixes { get; }
    }
}