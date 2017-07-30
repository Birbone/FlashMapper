using System.Linq.Expressions;
using System.Reflection;

namespace FlashMapper.DependancyInjection
{
    internal class ResolveExtraParameterStep : IMappingConfigStep
    {
        public ResolveExtraParameterStep(LambdaExpression resolveParameterExpression, MethodInfo nextStepConvertMethod, MethodInfo nextStepMapMethod, MethodInfo createMappingMethod)
        {
            ResolveParameterExpression = resolveParameterExpression;
            NextStepConvertMethod = nextStepConvertMethod;
            NextStepMapMethod = nextStepMapMethod;
            CreateMappingMethod = createMappingMethod;
        }

        public LambdaExpression ResolveParameterExpression { get; }
        public MethodInfo NextStepConvertMethod { get; }
        public MethodInfo NextStepMapMethod { get; }
        public MethodInfo CreateMappingMethod { get; }
    }
}