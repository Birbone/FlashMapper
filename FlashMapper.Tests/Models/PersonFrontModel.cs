using System;

namespace FlashMapper.Tests.Models
{
    public class PersonFrontModel
    {
        public int personId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal height { get; set; }
        public DateTime birthDate { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
    }
}