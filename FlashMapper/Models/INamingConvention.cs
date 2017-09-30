using System.Collections.Generic;

namespace FlashMapper.Models
{
    public interface INamingConvention
    {
        NamingConventionType NamingConventionType { get; }
        IEnumerable<string> Prefixes { get; }
    }

    public class DefaultNamingConvention : INamingConvention
    {
        public DefaultNamingConvention()
        {
            Prefixes = new List<string>();
        }

        public NamingConventionType NamingConventionType { get; set; }
        public List<string> Prefixes { get; set; }
        IEnumerable<string> INamingConvention.Prefixes => Prefixes;
    }
}