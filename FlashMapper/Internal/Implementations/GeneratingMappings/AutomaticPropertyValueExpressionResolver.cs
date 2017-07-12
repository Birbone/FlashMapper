using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;
using FlashMapper.Services.MatchingProperties;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class AutomaticPropertyValueExpressionResolver : IAutomaticPropertyValueExpressionResolver
    {
        private readonly IPropertyNameComparer propertyNameComparer;
        public AutomaticPropertyValueExpressionResolver(IPropertyNameComparer propertyNameComparer)
        {
            this.propertyNameComparer = propertyNameComparer;
        }

        private string GetPropertiesCollisionError(PropertyInfo destinationPropery, IEnumerable<PropertyInfo> matchingProperties)
        {
            var stringWriter = new StringWriter();
            stringWriter.WriteLine($"There are a few possible matches for property {destinationPropery.Name} in source model:");
            foreach (var matchingProperty in matchingProperties)
            {
                stringWriter.WriteLine(matchingProperty.Name);
            }
            return stringWriter.ToString();
        }

        public bool TryGetPropertyValueExpression<TSource, TDestination>(ParameterExpression source,
            PropertyInfo property,
            MemberBinding[] userBindings, 
            IFlashMapperSettings modelMapperSettings, 
            out Expression propertyValueExpression)
        {
            propertyValueExpression = null;
            var sourceType = typeof(TSource);
            var sourceProtperties = sourceType.GetProperties()
                .Where(p => p.CanRead)
                .ToArray();
            var rankedProperties = sourceProtperties.Select(
                    sp => new
                    {
                        Property = sp,
                        Rank = propertyNameComparer.Compare(property.Name, sp.Name, modelMapperSettings)
                    });

            var matchingProperties = rankedProperties.Where(pr => pr.Rank != PropertyNameCompareRank.DoNotMatch)
                .Where(pr => property.PropertyType.IsAssignableFrom(pr.Property.PropertyType))
                .OrderByDescending(pr => pr.Rank)
                .Select(pr => pr.Property)
                .ToArray();

            if (matchingProperties.Length == 0)
                return false;
            if (matchingProperties.Length > 1 && modelMapperSettings.CollisionBehavior != SelectSourceCollisionBehavior.ChooseAny)
                throw new PropertyIsNotMappedException(property.Name, GetPropertiesCollisionError(property, matchingProperties));
            var matchingProperty = matchingProperties[0];
            propertyValueExpression = Expression.Property(source, matchingProperty);
            if (property.PropertyType != matchingProperty.PropertyType)
                propertyValueExpression = Expression.Convert(propertyValueExpression, property.PropertyType);
            return true;
        }
    }
}