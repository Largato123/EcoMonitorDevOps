using Xunit;

namespace MonitoramentoEnergeticoAPI.Tests;

public class AlertaEnergiaControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AlertaEnergiaControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/alertaenergia");

        response.EnsureSuccessStatusCode();
    }
}
