using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrinugAspNetCoreWebApp;
using TrinugAspNetCoreWebApp.Tests;
using Xunit;

namespace ValueTest
{
    public class DeleteItem
    {
        private HttpClient Arrange(List<string> roles = null)
        {
            var testServer = new TestServer(WebHost.CreateDefaultBuilder()
            .UseStartup<StartupUnderTest>()
            .ConfigureServices(services =>
            {
                var mockRoles = new MockRoles();
                if (roles != null)
                {
                    mockRoles.AddRange(roles);
                }
                services.TryAddSingleton(mockRoles);
            })
            );
            return testServer.CreateClient();
        }

        [Fact]
        public async Task Without_Authorization_StatusCode_Is_Unauthorized()
        {
            //Arrange
            var client = Arrange();

            //Act
            var result = await client.DeleteAsync("/api/value/3");

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task User_Without_Proper_Role_StatusCode_Is_Forbidden()
        {
            //Arrange
            var client = Arrange(new List<string> { "DeletePerson" });
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/value/3");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fakeJwtTokenValue");

            //Act
            var result = await client.SendAsync(request);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task User_Without_Any_Roles_StatusCode_Is_Unauthorized()
        {
            //Arrange
            var client = Arrange();
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/value/3");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fakeJwtTokenValue");

            //Act
            var result = await client.SendAsync(request);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task User_With_Proper_Role_StatusCode_Is_Ok()
        {
            //Arrange
            var client = Arrange(new List<string> { "Delete" });
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/value/3");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fakeJwtTokenValue");

            //Act
            var result = await client.SendAsync(request);

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
