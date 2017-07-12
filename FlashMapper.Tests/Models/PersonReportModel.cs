namespace FlashMapper.Tests.Models
{
    public class PersonReportModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FullAddress => $"{Address}, {City}";
    }
}