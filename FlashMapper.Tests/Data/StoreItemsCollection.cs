using System;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class StoreItemsCollection
    {
        public static readonly StoreItem PencilData = new StoreItem
        {
            Id = new Guid("f7498204-8fec-4217-bc84-af1e8af6863b"),
            Address = "Store Address",
            Amount = 6,
            Name = "Pencil"
        };

        public StoreItem Pencil => PencilData;
        public StoreItem[] All => new[] {PencilData};
    }
}