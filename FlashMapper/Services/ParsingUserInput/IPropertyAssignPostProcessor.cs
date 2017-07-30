namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that looks for some specific assigns of destination model's properties and changes them
    /// </summary>
    public interface IPropertyAssignPostProcessor: ISpecificMapExpressionPostProcessor, IFlashMapperService
    {
    }
}