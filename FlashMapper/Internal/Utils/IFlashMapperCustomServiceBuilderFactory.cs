using System;

namespace FlashMapper.Internal.Utils
{
    [Obsolete]
    public interface IFlashMapperCustomServiceBuilderFactory
    {
        IFlashMapperCustomServiceBuilder Create();
    }
}