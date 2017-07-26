using System;

namespace FlashMapper.Tests.Models
{
    public class StoreItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
    }
}