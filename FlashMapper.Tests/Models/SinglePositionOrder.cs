using System;

namespace FlashMapper.Tests.Models
{
    public class SinglePositionOrder
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string Recipient { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
    }
}