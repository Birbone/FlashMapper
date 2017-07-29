using System;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class OrderPositionsCollection
    {
        public static readonly OrderPosition BrentOrdersSinglePencilData = new OrderPosition
        {
            Id = new Guid("d9fb12b0-4180-4e38-a291-a540d1a53f33"),
            Amount = 1,
            OrderId = OrdersCollection.BrentOrdersPencilData.Id,
            StoreItemId = StoreItemsCollection.PencilData.Id,
        };

        public OrderPosition BrentOrdersSinglePencil => BrentOrdersSinglePencilData;
        public OrderPosition[] All => new[] {BrentOrdersSinglePencilData};
    }
}