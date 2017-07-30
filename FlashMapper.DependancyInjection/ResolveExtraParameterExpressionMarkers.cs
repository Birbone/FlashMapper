using System;

namespace FlashMapper.DependancyInjection
{
    internal static class ResolveExtraParameterExpressionMarkers
    {
        public static string ResolveExtraParameterCall = $"{nameof(ResolveExtraParameterCall)}_{Guid.NewGuid()}";
        public static string ConvertNextStepCall = $"{nameof(ConvertNextStepCall)}_{Guid.NewGuid()}";
        public static string MapNextStepCall = $"{nameof(MapNextStepCall)}_{Guid.NewGuid()}";
    }
}