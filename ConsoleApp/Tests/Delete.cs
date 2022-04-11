namespace IntegrationTests.Models
{
    using FluentAssertions;
    using IntegrationTests;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;


    class Delete : BaseTest
    {
        [Test]
        public async Task Delete_An_Existing_Company_Machine()
        {
            var expectedMachine = new Test2
            {
                Description = "test1",
                Name = "test2",
                Location = "testova 3",
                WorkspaceId = "65"
            };
            var requestContent = new StringContent(expectedMachine.ToJson2(), Encoding.UTF8, "aplication/json");

            //Act
            var response = await _client.PostAsync("/api/machines/", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualMachine = Deserialize<AllMachines>.FromJson(responseContent);
            int id = actualMachine.Machine.Id;

            response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);


            var expectedAuthor = new Test
            {
                Id = id
            };

            //Act
            var response2 = await _client.DeleteAsync($"/api/machines/{expectedAuthor.Id}");
            response.EnsureSuccessStatusCode();

            var responseContent2 = await response.Content.ReadAsStringAsync();
            var actualAuthor = Deserialize<AllMachines>.FromJson(responseContent);

            actualAuthor.Status.Should().Equals("ok");
        }
    }
}
