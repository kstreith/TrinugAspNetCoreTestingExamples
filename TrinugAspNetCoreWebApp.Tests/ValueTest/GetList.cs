using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TrinugAspNetCoreWebApp;
using Xunit;

namespace ValueTest
{
    public class GetList
    {
        private HttpClient Arrange()
        {
            var testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            return testServer.CreateClient();
        }

        [Fact]
        public async Task StatusCode_Is_Ok()
        {
            //Arrange
            var client = Arrange();

            //Act
            var result = await client.GetAsync("/api/value");

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ContentType_Is_Json()
        {
            //Arrange
            var client = Arrange();

            //Act
            var result = await client.GetAsync("/api/value");

            //Assert
            (result.Content.Headers.ContentType?.MediaType).Should().Be("application/json");
            var resultContent = await result.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<string>>(resultContent);
            list.Should().NotBeNull();
            list.Should().BeEquivalentTo("value1", "value2");
        }

        [Fact]
        public async Task Returns_Correct_List()
        {
            //Arrange
            var client = Arrange();

            //Act
            var result = await client.GetAsync("/api/value");

            //Assert
            var resultContent = await result.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<string>>(resultContent);
            list.Should().BeEquivalentTo("value1", "value2");
        }
    }
}
