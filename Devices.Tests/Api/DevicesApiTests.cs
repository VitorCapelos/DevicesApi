using Devices.Api;
using Devices.Api.Models;
using Devices.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Devices.Tests.Api
{
    public class DevicesApiTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        public DevicesApiTests(WebApplicationFactory<Program> webAppFactory) => _webAppFactory = webAppFactory;

        [Fact]
        public async Task CreateAndGetDevice()
        {
            var client = _webAppFactory.CreateClient();
            var createDevice = new DeviceCreateDto("TestDevice", "TestBrand", DeviceState.Available);
            var response = await client.PostAsJsonAsync("/api/devices", createDevice);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
