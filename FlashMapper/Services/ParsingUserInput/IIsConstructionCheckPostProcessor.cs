namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that replaces MappingOptions.IsConstruction() call in expression with an approprieate value
    /// </summary>
    public interface IIsConstructionCheckPostProcessor: ISpecificMapExpressionPostProcessor, IFlashMapperService
    {
    }
}