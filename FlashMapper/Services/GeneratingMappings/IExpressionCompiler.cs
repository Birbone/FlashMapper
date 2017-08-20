using System;
using System.Linq.Expressions;

namespace FlashMapper.Services.GeneratingMappings
{
    public interface IExpressionCompiler : IFlashMapperService
    {
        TDelegate Compile<TDelegate>(Expression<TDelegate> expression);
    }
}