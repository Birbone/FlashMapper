using System.Collections.Generic;
using System.Linq;

namespace FlashMapper.Models
{
    public class NamingConvention: INamingConvention
    {
        private readonly string[] prefixes;

        public NamingConvention(NamingConventionType namingConventionType, params string[] prefixes)
        {
            this.prefixes = prefixes.ToArray();
            NamingConventionType = namingConventionType;
        }

        public NamingConventionType NamingConventionType { get; }
        public IEnumerable<string> Prefixes => prefixes;
    }
}