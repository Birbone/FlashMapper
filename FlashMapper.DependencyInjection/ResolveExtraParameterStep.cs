using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FlashMapper.DependencyInjection
{
    internal class ResolveExtraParameterStep : IMappingConfigStep
    {
        public ResolveExtraParameterStep(LambdaExpression resolveParameterExpression, 
            MethodInfo nextStepConvertMethod, 
            MethodInfo nextStepMapMethod, 
            MethodInfo createMappingMethod, 
            Type configuratorType)
        {
            ResolveParameterExpression = resolveParameterExpression;
            NextStepConvertMethod = nextStepConvertMethod;
            NextStepMapMethod = nextStepMapMethod;
            CreateMappingMethod = createMappingMethod;
            ConfiguratorType = configuratorType;
        }

        public LambdaExpression ResolveParameterExpression { get; }
        public MethodInfo NextStepConvertMethod { get; }
        public MethodInfo NextStepMapMethod { get; }
        public MethodInfo CreateMappingMethod { get; }
        public Type ConfiguratorType { get; }
    }
}