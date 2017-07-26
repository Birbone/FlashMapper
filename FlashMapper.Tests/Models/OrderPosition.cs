using System;

namespace FlashMapper.Tests.Models
{
    public class OrderPosition
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Guid OrderId { get; set; }
        public Guid StoreItemId { get; set; }
    }
}