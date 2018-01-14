using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class ExpressionCompiler : IExpressionCompiler
    {
        public TDelegate Compile<TDelegate>(Expression<TDelegate> expression)
        {
            return expression.Compile();
        }
    }
}