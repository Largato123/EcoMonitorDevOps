using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MonitoramentoEnergeticoAPI.Tests;

public class SetorControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SetorControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHttpStatusCode200()
    {
        var request = "/api/setor";

        var response = await _client.GetAsync(request);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
