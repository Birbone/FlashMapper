namespace FlashMapper.Models
{
    public class MappingPostProcessingContext
    {
        public MappingPostProcessingContext(bool isConstruction)
        {
            IsConstruction = isConstruction;
        }

        public bool IsConstruction { get; }
    }
}