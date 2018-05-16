using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MvcTestingExample.Tests
{
    public class TestHomePage : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TestHomePage(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Returns_HtmlPage_With_StatusCode_OK()
        {
            //Arrange

            //Act
            var result = await _client.GetAsync("/");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("text/html", result.Content.Headers.ContentType.MediaType);
            var response = await result.Content.ReadAsStringAsync();
        }
    }
}
