namespace FlashMapper.Services.ParsingUserInput
{
    /// <summary>
    /// Service that checks if Expression node is call of a method <see cref="MappingOptions.Ignore"/>
    /// </summary>
    public interface IImplicitIgnoreNodeChecker: ISpecificIgnoreNodeChecker, IFlashMapperService
    {
    }
}