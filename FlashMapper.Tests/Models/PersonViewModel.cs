using System;

namespace FlashMapper.Tests.Models
{
    public class PersonViewModel
    {
        public int? PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}