using System;
using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class OrdersCollection
    {
        public static readonly Order BrentOrdersPencilData = new Order
        {
            Id = new Guid("413ee994-ea5f-4223-bb80-a5386c2e7267"),
            Address = "32 Red Fox Dr",
            OrDernumBer = "What is this field for?",
            OrderNumber = "123456",
            Recipient = "Brent"
        };

        public Order BrentOrdersPencil => BrentOrdersPencilData;
        public Order[] All => new[] {BrentOrdersPencilData};
    }
}