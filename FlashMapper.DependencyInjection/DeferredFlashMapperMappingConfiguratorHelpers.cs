using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlashMapper.DependencyInjection
{
    internal static class DeferredFlashMapperMappingConfiguratorHelpers
    {
        public static object CloneClosure<TBuilder>(Type targetType, IEnumerable<FieldInfo> thisFields,
            IDictionary<FieldInfo, object> otherFields, TBuilder currentBuilder)
        {
            var newTarget = Activator.CreateInstance(targetType);
            foreach (var thisField in thisFields)
            {
                thisField.SetValue(newTarget, currentBuilder);
            }
            foreach (var otherField in otherFields)
            {
                otherField.Key.SetValue(newTarget, otherField.Value);
            }
            return newTarget;
        }

        public static bool IsClosure<TBuilder>(object target, TBuilder capturedBuilder)
        {
            var targetType = target.GetType();
            var constructors = targetType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (constructors.Length != 1)
                return false;
            if (constructors[0].GetParameters().Length != 0)
                return false;
            return targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(f => f.FieldType == typeof(TBuilder) && capturedBuilder.Equals(f.GetValue(target)));
        }

        public static void GetClosureDefinition<TBuilder>(object target, TBuilder capturedBuilder, out Type targetType, out IEnumerable<FieldInfo> thisFields, out IDictionary<FieldInfo, object> otherFields)
        {
            thisFields = null;
            otherFields = null;
            targetType = target.GetType();
            var allFields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var thisFieldsArray = allFields
                .Where(f => f.FieldType == typeof(TBuilder))
                .Where(f => capturedBuilder.Equals(f.GetValue(target)))
                .ToArray();
            thisFields = thisFieldsArray;
            otherFields = allFields.Where(f => !thisFieldsArray.Contains(f))
                .ToDictionary(f => f, f => f.GetValue(target));
        }
    }
}