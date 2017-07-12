using System;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class MappingGenerator : IMappingGenerator
    {
        private readonly IMappingExpressionAutocompleteService mappingExpressionAutocompleteService;

        public MappingGenerator(IMappingExpressionAutocompleteService mappingExpressionAutocompleteService)
        {
            this.mappingExpressionAutocompleteService = mappingExpressionAutocompleteService;
        }

        public Mapping<TSource, TDestination> GenerateCompleteMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> userInput,
            IFlashMapperSettings settings)
        {
            var buildExpression = mappingExpressionAutocompleteService.CompleteBuildExpression(userInput, settings);
            var mapDataExpression = mappingExpressionAutocompleteService.CompleteMapDataExpression(userInput, settings);
            var buildMethod = buildExpression.Compile();
            var mapDataMethod = mapDataExpression.Compile();
            return new Mapping<TSource, TDestination>(buildMethod, mapDataMethod, settings);
        }
    }
}