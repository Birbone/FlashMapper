using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FlashMapper.Services.ParsingUserInput;

namespace FlashMapper.Internal.Implementations.ParsingUserInput
{
    public class PropertyAssignPostProcessorVisitor : ExpressionVisitor
    {
        private readonly IEnumerable<ISpecificPropertyAssignMapExpressionBuilderFactory> specificExpressionBuilderFactories;

        public PropertyAssignPostProcessorVisitor(IEnumerable<ISpecificPropertyAssignMapExpressionBuilderFactory> specificExpressionBuilderFactories)
        {
            this.specificExpressionBuilderFactories = specificExpressionBuilderFactories;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Assign)
                return base.VisitBinary(node);
            IMapExpressionBuilder expressionBuilder = null;
            if (!specificExpressionBuilderFactories.Any(f => f.TryCreate(node.Left, node.Right, out expressionBuilder)))
                return base.VisitBinary(node);

            return expressionBuilder.GetExpression();
        }
    }
}