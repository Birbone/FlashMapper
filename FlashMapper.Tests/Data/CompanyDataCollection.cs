using FlashMapper.Tests.Models;

namespace FlashMapper.Tests.Data
{
    public class CompanyDataCollection
    {
        private static readonly CompanyData SomeCompanyDataData = new CompanyData
        {
            DataForUIF = "4e1d3fad-0b06-478e-8bd9-8dff631c1fdf",
            GIDData = "d318fa94-97d7-4ea3-a514-0ec7cafc6cd9",
            MKDIT = "0a54a245-73f3-429d-b78f-42157cb48987"
        };

        public CompanyData SomeCompanyData => SomeCompanyDataData;
    }
}