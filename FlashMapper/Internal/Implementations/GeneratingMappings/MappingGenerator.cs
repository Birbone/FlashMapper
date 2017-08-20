using System;
using System.Linq.Expressions;
using FlashMapper.Models;
using FlashMapper.Services.GeneratingMappings;

namespace FlashMapper.Internal.Implementations.GeneratingMappings
{
    public class MappingGenerator : IMappingGenerator
    {
        private readonly IMappingExpressionAutocompleteService mappingExpressionAutocompleteService;
        private readonly IExpressionCompiler expressionCompiler;

        public MappingGenerator(IMappingExpressionAutocompleteService mappingExpressionAutocompleteService, IExpressionCompiler expressionCompiler)
        {
            this.mappingExpressionAutocompleteService = mappingExpressionAutocompleteService;
            this.expressionCompiler = expressionCompiler;
        }
        
        public Mapping<TSource, TDestination> GenerateCompleteMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> userInput,
            IFlashMapperSettings settings)
        {
            var buildExpression = mappingExpressionAutocompleteService.CompleteBuildExpression(userInput, settings);
            var mapDataExpression = mappingExpressionAutocompleteService.CompleteMapDataExpression(userInput, settings);
            var buildMethod = expressionCompiler.Compile(buildExpression);
            var mapDataMethod = expressionCompiler.Compile(mapDataExpression);
            return new Mapping<TSource, TDestination>(buildMethod, mapDataMethod, settings);
        }
    }
}