using System.Linq.Expressions;
using System.Reflection;

namespace FlashMapper.DependancyInjection
{
    internal class ResultMappingConfigStep : IMappingConfigStep
    {
        public ResultMappingConfigStep(LambdaExpression resultExpression, MethodInfo createMappingMethod)
        {
            ResultExpression = resultExpression;
            CreateMappingMethod = createMappingMethod;
        }

        public LambdaExpression ResultExpression { get; }
        public MethodInfo CreateMappingMethod { get; }
    }
}