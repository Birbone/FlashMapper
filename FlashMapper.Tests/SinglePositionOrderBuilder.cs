using FlashMapper.DependancyInjection;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    public class SinglePositionOrderBuilder : FlashMapperBuilder<OrderPosition, SinglePositionOrder, SinglePositionOrderBuilder>
    {
        public SinglePositionOrderBuilder(IMappingConfiguration mappingConfiguration) : base(mappingConfiguration)
        {
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<OrderPosition, SinglePositionOrder> configurator)
        {
            //configurator.ResolveExtraParameter(op => TestData.Orders.All.First(o => o.Id == op.OrderId))
            //    .ResolveExtraParameter((op, o) => TestData.StoreItems.All.First(si => si.Id == op.StoreItemId))
            //    .ConfigureMapping((op, o, si) => new SinglePositionOrder
            //    {
            //        Id = o.Id,
            //    });
        }
    }
}