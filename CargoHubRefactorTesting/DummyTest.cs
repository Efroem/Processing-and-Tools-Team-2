using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using CargoHubRefactor;

namespace CargoHubRefactorTesting;

public class DummyTest : IClassFixture<WebApplicationFactory<CargoHubRefactor.Program>>{
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<CargoHubRefactor.Program> _factory;

    
        public DummyTest(WebApplicationFactory<CargoHubRefactor.Program> factory)
        {
            _factory = factory;
        }

    [Fact]
    public async Task GetDummyTest() {
        var _client = _factory.CreateClient();
        var response = await _client.GetAsync($"/dummy");
        Assert.True(response.IsSuccessStatusCode);
    }


}