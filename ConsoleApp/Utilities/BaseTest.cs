using NUnit.Framework;
using System;
using System.Net.Http;

namespace IntegrationTests
{
    [TestFixture]
    public class BaseTest
    {
        public HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.stuk.help")
            };

            _client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "EIWtfZiWQCmXXZSqwEFfJjTr1ufzBMves0XKhbfGpjECCoiKkXpleZNHvR6X");
        }
    }
}
