using AutoMapper;
using FlashMapper.PerformanceTests.Models;

namespace FlashMapper.PerformanceTests.Services.IgnoreTest
{
    public class AutoMapperIgnoreTestParticipant : IPerformanceTestParticipant<IgnoreTestSource>
    {
        private IMapper mapper;
        private readonly Destination destination;
        public AutoMapperIgnoreTestParticipant()
        {
            destination = new Destination();
        }

        public string Name => "AutoMapper";
        public void Initialize()
        {
            var mapperConfiguration = new MapperConfiguration(c => c
                .CreateMap<IgnoreTestSource, Destination>()
                .ForMember(d => d.Data2, o => o.Ignore())
                .ForMember(d => d.Data4, o => o.Ignore())
                .ForMember(d => d.Data5, o => o.Condition(s => s.Data3 > 0.5))
                .ForMember(d => d.Data7, o => o.Ignore())
                .ForMember(d => d.EighthData, o => o.Ignore()));
            mapper = mapperConfiguration.CreateMapper();
        }

        public void Convert(IgnoreTestSource source)
        {
            mapper.Map<IgnoreTestSource, Destination>(source);
        }

        public void MapData(IgnoreTestSource source)
        {
            mapper.Map(source, destination);
        }
    }
}
