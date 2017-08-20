using AutoMapper;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IdenticalModelsTest
{
    public class AutoMapperIdenticalTestParticipant : IPerformanceTestParticipant<IdenticalTestSource>
    {
        private IMapper mapper;
        private readonly Destination destination;
        public AutoMapperIdenticalTestParticipant()
        {
            destination = new Destination();
        }

        public string Name => "AutoMapper";
        public void Initialize()
        {
            var mapperConfiguration = new MapperConfiguration(c => c.CreateMap<IdenticalTestSource, Destination>());
            mapper = mapperConfiguration.CreateMapper();
        }

        public void Convert(IdenticalTestSource source)
        {
            mapper.Map<IdenticalTestSource, Destination>(source);
        }

        public void MapData(IdenticalTestSource source)
        {
            mapper.Map(source, destination);
        }
    }
}