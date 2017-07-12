namespace FlashMapper.Models
{
    public class MappingCombination<TSourceValue, TDestinationValue>
    {
        public TSourceValue Source { get; set; }
        public TDestinationValue Destination { get; set; }
    }
}