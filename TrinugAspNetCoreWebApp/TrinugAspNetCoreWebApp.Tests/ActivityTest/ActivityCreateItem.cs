using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrinugAspNetCoreWebApp;
using TrinugAspNetCoreWebApp.Repository;
using TrinugAspNetCoreWebApp.Tests.Mocks;
using Xunit;

namespace ActivityTest
{
    public class ActivityCreateItem
    {
        private HttpClient Arrange()
        {
            var testServer = new TestServer(
                WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IActivityDataRepository, MockActivityDataRepository>();
                    services.AddSingleton<ILocationDataRepository, MockLocationDataRepository>();
                })
            );
            return testServer.CreateClient();
        }

        [Fact]
        public async Task Success_StatusCode_Is_Ok()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject(new
            {
                Name = "TRINUG Meetup",
                Location = "AisOffice",
                AttendeeLimit = 50
            }), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/activity", reqContent);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task InvalidLocation_StatusCode_Is_BadRequest()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject(new
            {
                Name = "TRINUG Meetup",
                Location = "UnknownLocation",
                AttendeeLimit = 50
            }), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/activity", reqContent);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task InvalidLocation_Returns_Error_List()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject(new
            {
                Name = "TRINUG Meetup",
                Location = "UnknownLocation",
                AttendeeLimit = 50
            }), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/activity", reqContent);

            //Assert
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultList = JsonConvert.DeserializeObject<List<string>>(resultContent);
            resultList.Should().BeEquivalentTo("Location 'UnknownLocation' does not exist");
        }

    }
}
