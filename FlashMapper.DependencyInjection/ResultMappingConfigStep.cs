using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FlashMapper.DependencyInjection
{
    internal class ResultMappingConfigStep : IMappingConfigStep
    {
        public ResultMappingConfigStep(LambdaExpression resultExpression, MethodInfo createMappingMethod, Delegate settingsInitializeDelegate)
        {
            ResultExpression = resultExpression;
            CreateMappingMethod = createMappingMethod;
            SettingsInitializeDelegate = settingsInitializeDelegate;
        }

        public LambdaExpression ResultExpression { get; }
        public MethodInfo CreateMappingMethod { get; }
        public Delegate SettingsInitializeDelegate { get; }
    }
}