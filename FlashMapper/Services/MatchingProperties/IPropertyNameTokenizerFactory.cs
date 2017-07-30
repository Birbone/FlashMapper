namespace FlashMapper.Services.MatchingProperties
{
    public interface IPropertyNameTokenizerFactory
    {
        /// <summary>
        /// Checks if factory corresponds to specified <see cref="namingConvention"/> and creates tokenizer if so
        /// </summary>
        /// <param name="namingConvention">Specified naming convention</param>
        /// <param name="propertyNameTokenizer">Tokenizer that relates to factory</param>
        /// <returns></returns>
        bool TryCreate(NamingConventionType namingConvention, out IPropertyNameTokenizer propertyNameTokenizer);
    }
}