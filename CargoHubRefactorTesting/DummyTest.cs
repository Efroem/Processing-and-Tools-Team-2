using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace CargoHubRefactorTesting;

public class DummyTest : IClassFixture<WebApplicationFactory<Program>>{
        // private readonly HttpClient _client;

        // public DummyTest(WebApplicationFactory<Program> factory)
        // {
        //     _client = factory.CreateClient();
        // }

    [Fact]
    public async Task GetDummyTest() {
        // var response = await _client.GetAsync($"/dummy");

        // Assert.True(response.IsSuccessStatusCode);
        Assert.True(true);
    }
}