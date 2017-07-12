using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlashMapper.MultiSource
{
    public class ParameterReplacementVisitor : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, Expression> parameterReplacementMap;

        public ParameterReplacementVisitor(IDictionary<ParameterExpression, Expression> parameterReplacementMap)
        {
            this.parameterReplacementMap = parameterReplacementMap;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression result;
            if (parameterReplacementMap.TryGetValue(node, out result))
                return result;
            return base.VisitParameter(node);
        }
    }
}