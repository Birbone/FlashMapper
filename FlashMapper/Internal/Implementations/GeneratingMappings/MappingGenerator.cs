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
        private readonly IFlashMapperSettings settings;

        public MappingGenerator(IMappingExpressionAutocompleteService mappingExpressionAutocompleteService,
            IExpressionCompiler expressionCompiler, 
            IFlashMapperSettings settings)
        {
            this.mappingExpressionAutocompleteService = mappingExpressionAutocompleteService;
            this.expressionCompiler = expressionCompiler;
            this.settings = settings;
        }
        
        public Mapping<TSource, TDestination> GenerateCompleteMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> userInput, IFlashMapperMappingCallbacks<TSource, TDestination> callbacks)
        {
            var buildExpression = mappingExpressionAutocompleteService.CompleteBuildExpression(userInput, callbacks);
            var mapDataExpression = mappingExpressionAutocompleteService.CompleteMapDataExpression(userInput, callbacks);
            var buildMethod = expressionCompiler.Compile(buildExpression);
            var mapDataMethod = expressionCompiler.Compile(mapDataExpression);
            return new Mapping<TSource, TDestination>(buildMethod, mapDataMethod, settings);
        }
    }
}