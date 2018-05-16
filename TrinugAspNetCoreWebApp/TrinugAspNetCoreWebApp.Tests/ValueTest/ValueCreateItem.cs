using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrinugAspNetCoreWebApp;
using Xunit;

namespace ValueTest
{
    public class ValueCreateItem
    {
        private HttpClient Arrange()
        {
            var testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            return testServer.CreateClient();
        }

        [Fact]
        public async Task Success_StatusCode_Is_Ok()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject("test"), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/value", reqContent);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ValidationFailure_StatusCode_Is_BadRequest()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject("bad"), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/value", reqContent);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ValidationFailure_Returns_Error_List()
        {
            //Arrange
            var client = Arrange();
            var reqContent = new StringContent(JsonConvert.SerializeObject("bad"), Encoding.UTF8, "application/json");

            //Act
            var result = await client.PostAsync("/api/value", reqContent);

            //Assert
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultList = JsonConvert.DeserializeObject<List<string>>(resultContent);
            resultList.Should().BeEquivalentTo("Input cannot have a value of 'bad'");
        }

    }
}
