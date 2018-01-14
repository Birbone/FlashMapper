using System.Linq.Expressions;

namespace FlashMapper.DependencyInjection
{
    internal class ResolveExtraParameterBlocks
    {
        public ParameterExpression ExtraParameterVariable { get; set; }
        public Expression ExtraParameterInitialization { get; set; }
        public Expression ConvertNextStepCall { get; set; }
        public ParameterExpression ResultVariable { get; set; }
        public Expression MapNextStepCall { get; set; }
    }
}