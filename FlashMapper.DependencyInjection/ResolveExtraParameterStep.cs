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
            IServiceRegistrationHelper serviceRegistrationHelper)
        {
            ResolveParameterExpression = resolveParameterExpression;
            NextStepConvertMethod = nextStepConvertMethod;
            NextStepMapMethod = nextStepMapMethod;
            CreateMappingMethod = createMappingMethod;
            ServiceRegistrationHelper = serviceRegistrationHelper;
        }

        public LambdaExpression ResolveParameterExpression { get; }
        public MethodInfo NextStepConvertMethod { get; }
        public MethodInfo NextStepMapMethod { get; }
        public MethodInfo CreateMappingMethod { get; }
        public IServiceRegistrationHelper ServiceRegistrationHelper { get; }
    }
}