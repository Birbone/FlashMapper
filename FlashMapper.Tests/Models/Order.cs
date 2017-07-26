using System;

namespace FlashMapper.Tests.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrDernumBer { get; set; }
        public string Recipient { get; set; }
        public string Address { get; set; }
    }
}