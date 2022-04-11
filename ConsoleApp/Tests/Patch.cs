namespace IntegrationTests
{
    using FluentAssertions;
    using IntegrationTests.Models;
    using MongoDB.Bson;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class PatchTests : BaseTest
    {
        [Test]
        public async Task ChangePassword_ShouldReturnSuccess()
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Name = "test79",
                Email = "krasibalboa@abv.bg",
                Phone = "0999999999",
                Workspace = "test7m",
                Language = "en",
                Password = "00000001",
                PasswordConfirmation = "00000001",
                WorkspaceId = 65
            };

            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");
            //Act
            var response = await _client.PatchAsync("/api/me/", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
        }

        [Test]
        public async Task UpdateACompanyМachine()
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Name = "test79",
                Location = "test streed 7",
                Description = "test description very good machine",
                WorkspaceId = 65,
                Id = 692
            };

            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");
            //Act
            var response = await _client.PatchAsync($"/api/machines/{expectedAuthor.Id}", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
        }
    }
}