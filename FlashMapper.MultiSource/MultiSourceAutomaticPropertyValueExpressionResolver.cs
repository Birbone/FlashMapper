using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.MultiSource
{
    public class MultiSourceAutomaticPropertyValueExpressionResolver : IAutomaticPropertyValueExpressionResolver
    {
        private readonly IPropertyNameComparer propertyNameComparer;

        public MultiSourceAutomaticPropertyValueExpressionResolver(IPropertyNameComparer propertyNameComparer)
        {
            this.propertyNameComparer = propertyNameComparer;
        }

        private string GetPropertiesCollisionError(PropertyInfo destinationPropery, IEnumerable<RankedProperty> matchingProperties)
        {
            var stringWriter = new StringWriter();
            stringWriter.WriteLine($"There are a few possible matches for property {destinationPropery.Name} in source model:");
            foreach (var matchingProperty in matchingProperties)
            {
                stringWriter.WriteLine($"<{matchingProperty.WrapProperty.PropertyType}>{matchingProperty.WrapProperty.Name}.{matchingProperty.Property.Name}");
            }
            return stringWriter.ToString();
        }

        public bool TryGetPropertyValueExpression<TSource, TDestination>(ParameterExpression source, PropertyInfo property,
            MemberBinding[] userBindings, IFlashMapperSettings modelMapperSettings, out Expression propertyValueExpression)
        {
            propertyValueExpression = null;
            var sourcesWrapType = typeof(TSource);
            var sourcesWrapProperties = sourcesWrapType
                .GetProperties();
            var rankedProperties = new List<RankedProperty>();
            var sourceIndex = 0;
            foreach (var sourcesWrapProperty in sourcesWrapProperties)
            {
                sourceIndex++;
                var sourceType = sourcesWrapProperty.PropertyType;
                var sourceProtperties = sourceType.GetProperties()
                    .Where(p => p.CanRead)
                    .ToArray();
                var sourceRankedProperties = sourceProtperties.Select(
                        sp => new RankedProperty
                        {
                            Property = sp,
                            Rank = propertyNameComparer.Compare(property.Name, sp.Name, modelMapperSettings),
                            SourceIndex = sourceIndex,
                            WrapProperty = sourcesWrapProperty
                        });
                rankedProperties.AddRange(sourceRankedProperties);
            }
            var matchingProperties = rankedProperties.Where(pr => pr.Rank != PropertyNameCompareRank.DoNotMatch)
                .Where(pr => property.PropertyType.IsAssignableFrom(pr.Property.PropertyType))
                .OrderByDescending(pr => pr.Rank)
                .ThenBy(pr => pr.SourceIndex)
                .ToArray();
            if (matchingProperties.Length == 0)
                return false;
            if (matchingProperties.Length > 1 && modelMapperSettings.CollisionBehavior != SelectSourceCollisionBehavior.ChooseAny)
                throw new PropertyIsNotMappedException(property.Name, GetPropertiesCollisionError(property, matchingProperties));
            var matchingProperty = matchingProperties[0];
            var sourceProperty = Expression.Property(source, matchingProperty.WrapProperty);
            propertyValueExpression = Expression.Property(sourceProperty, matchingProperty.Property);
            if (property.PropertyType != matchingProperty.Property.PropertyType)
                propertyValueExpression = Expression.Convert(propertyValueExpression, property.PropertyType);
            return true;
        }

        private class RankedProperty
        {
            public PropertyInfo WrapProperty { get; set; }
            public PropertyInfo Property { get; set; }
            public PropertyNameCompareRank Rank { get; set; }
            public int SourceIndex { get; set; }
        }
    }
}