namespace FlashMapper.Tests.Models
{
    public class PersonWithCtor
    {
        public PersonWithCtor(string firstName, string lastName, int personId)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonId = personId;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int PersonId { get; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}