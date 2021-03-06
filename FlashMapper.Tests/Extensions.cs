using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using FlashMapper.Models;

namespace FlashMapper.Tests
{
    internal static class Extensions
    {
        public static IEnumerable<MappingCombination<TSource, TDestination>> GetAllCombinations<TSource, TDestination>(
            this IEnumerable<TSource> sources, IEnumerable<TDestination> destinations)
        {
            sources = sources.Distinct();
            destinations = destinations.Distinct()
                .ToArray();
            foreach (var source in sources)
            {
                foreach (var destination in destinations)
                {
                    yield return new MappingCombination<TSource, TDestination>
                    {
                        Source = source,
                        Destination = destination
                    };
                }
            }
        }

        public static IEnumerable<TModel> With<TModel>(this IEnumerable<TModel> models, TModel anotherModel)
        {
            foreach (var model in models)
                yield return model;
            yield return anotherModel;
        }

        public static bool In<TModel>(this TModel model, IEnumerable<TModel> collection)
        {
            return collection.Contains(model);
        }

        public static bool In<TModel>(this TModel model, params TModel[] collection)
        {
            return In(model, (IEnumerable<TModel>)collection);
        }

        public static bool In(this string str, StringComparison stringComparisonOptions, IEnumerable<string> stringCollection)
        {
            if (str == null)
                return In<string>(null, stringCollection);

            return stringCollection.Any(s => str.Equals(s, stringComparisonOptions));
        }

        public static bool In(this string str, StringComparison stringComparisonOptions,
            params string[] stringCollection)
        {
            return In(str, stringComparisonOptions, (IEnumerable<string>) stringCollection);
        }
    }
}
