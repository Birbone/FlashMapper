using System;
using System.Linq.Expressions;
using FlashMapper.Models;

namespace FlashMapper.Services.GeneratingMappings
{
    public interface IMappingGenerator : IFlashMapperService
    {
        Mapping<TSource, TDestination> GenerateCompleteMapping<TSource, TDestination>(Expression<Func<TSource, TDestination>> userInput);
    }
}