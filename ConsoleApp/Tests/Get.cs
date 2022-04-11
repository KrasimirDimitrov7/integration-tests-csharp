namespace Tests
{
    using FluentAssertions;
    using IntegrationTests;
    using IntegrationTests.Models;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System.Linq;

    public class GetTests : BaseTest
    {
        [Test]
        public async Task GetAuthor_WithValidId_ShouldReturnSuccess()
        {
            var response = await _client.GetAsync("/api/me");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(content);
            var actualAuthor = Deserialize<LogIn>.FromJson(content);
            //var last = actualAuthor.Last();

        }

        [Test]
        public async Task Get_All_Company_Machines()
        {
            var expectedAuthor = new Test
            {
                WorkspaceId = 65
            };

            //Act
            var response = await _client.GetAsync($"/api/machines/?workspace_id=65");
            response.EnsureSuccessStatusCode();


            var content = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(content);
            var listMachines = Deserialize<AllMachines>.FromJson(content);

            string Location = listMachines.Machines[0].Location;
            System.Console.WriteLine(Location);

            var lastMachine = listMachines.Machines.Last();
            //Assert.AreEqual(706, lastMachine.Id);

            var numberManchines = listMachines.Machines.Count;
            System.Console.WriteLine(numberManchines);

            Assert.That(listMachines.Machines.Count > 0);
            Assert.AreEqual(692, listMachines.Machines[0].Id);
            Assert.AreEqual("235215020821", listMachines.Machines[0].Hash);
            Assert.AreEqual("test79", listMachines.Machines[0].Name);
            Assert.AreEqual("test streed 7", listMachines.Machines[0].Location);
        }

        [Test]
        public async Task Get_An_Existing_Company_Machine()
        {
            var expectedAuthor = new Test
            {
                Id = 692
            };

            //Act
            var response = await _client.GetAsync($"/api/machines/{expectedAuthor.Id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var actualAuthor = Deserialize<AllMachines>.FromJson(content);

            actualAuthor.Status.Should().Equals("ok");

        }


        [Test]
        public async Task Get_Company_Billing_Information()
        {
            //Act
            var response = await _client.GetAsync($"/api/billing/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var actualAuthor = Deserialize<AllMachines>.FromJson(content);

            Assert.AreEqual(actualAuthor.Status, "ok");
            Assert.AreEqual(actualAuthor.Msg, "Get Company Billing");
        }
    }
}
