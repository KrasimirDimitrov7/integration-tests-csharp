namespace IntegrationTests
{
    using FluentAssertions;
    using IntegrationTests.Models;
    using MongoDB.Bson;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class PostTests : BaseTest
    {
        [Order(1)]
        [Test]
        public async Task LogInUser_ShouldReturnSuccess()
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Email = "krasibalboa@abv.bg",
                Password = "00000001"
            };

            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");
            //Act
            var response = await _client.PostAsync("/api/login/", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualAuthor = Deserialize<LogIn>.FromJson(responseContent);

            //Assert
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            actualAuthor.Status.Should().Be("ok");
        }

        [Order(2)]
        [Test]
        [TestCase("krasibalboa11@abv.bg", "00000001")]
        [TestCase("krasibalboa@abv.bg", "00000002")]

        public async Task LogInUser_WithInvalidEmailAndPassword_ShouldReturnBadRequest(string email, string pass)
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Email = email,
                Password = pass
            };
            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");

            //Act
            var response = await _client.PostAsync("/api/login/", requestContent);
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)403);
        }

        [Order(3)]
        [Test]
        public async Task LogInUser_WithInvalidPassword_ShouldReturnBadRequest()
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Email = "krasibalboa@abv.bg",
                Password = "00000002"
            };
            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");

            //Act
            var response = await _client.PostAsync("/api/login/", requestContent);
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)403);
        }

        [Order(4)]
        [Test]
        public async Task LogIn_WithNoData_ShouldReturnInternalServerError()
        {
            var response = await _client.PostAsync("/api/login/", null);
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
        }

        [Test]
        public async Task Resend_Verification_Email()
        {
            //Arrange
            var expectedAuthor = new Test
            {
                Email = "cekokikj729k3@5lsword.com"
            };
            var requestContent = new StringContent(expectedAuthor.ToJson(), Encoding.UTF8, "aplication/json");

            //Act
            var response = await _client.PostAsync("/api/email/resend", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualAuthor = Deserialize<AllMachines>.FromJson(responseContent);
            response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
        }

        //[Test]
        //public async Task Create_A_Company_Machine()
        //{
        //    //Arrange
        //    var expectedMachine = new Test2
        //    {
        //        Description = "test1",
        //        Name = "test2",
        //        Location = "testova 3",
        //        WorkspaceId = "65"
        //    };
        //    var requestContent = new StringContent(expectedMachine.ToJson2(), Encoding.UTF8, "aplication/json");

        //    //Act
        //    var response = await _client.PostAsync("/api/machines/", requestContent);
        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    var actualMachine = Deserialize<AllMachines>.FromJson(responseContent);

        //    response.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
        //}
    }
}



