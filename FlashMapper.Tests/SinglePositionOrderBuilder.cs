using System;
using System.Linq;
using FlashMapper.DependencyInjection;
using FlashMapper.Tests.Data;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests
{
    public class SinglePositionOrderBuilder : FlashMapperBuilder<OrderPosition, SinglePositionOrder, SinglePositionOrderBuilder>
    {
        private readonly int recipientId;

        public SinglePositionOrderBuilder(IMappingConfiguration mappingConfiguration, int recipientId): base(mappingConfiguration)
        {
            this.recipientId = recipientId;
        }

        protected override void ConfigureMapping(IFlashMapperBuilderConfigurator<OrderPosition, SinglePositionOrder> configurator)
        {
            configurator.ResolveExtraParameter(op => TestData.Orders.All.First(o => o.Id == op.OrderId))
                .ResolveExtraParameter((op, o) => TestData.StoreItems.All.First(si => si.Id == op.StoreItemId))
                .ResolveExtraParameter((op, o, si) => TestData.People.All.First(p => p.PersonId == recipientId))
                .CreateMapping((op, o, si, r) => new SinglePositionOrder
                {
                    Id = o.Id,
                    Address = r.Address
                })
                .CollisionBehavior(SelectSourceCollisionBehavior.ChooseAny);
        }
    }
}