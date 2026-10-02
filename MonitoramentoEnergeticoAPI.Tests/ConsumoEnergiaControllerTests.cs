using Xunit;

namespace MonitoramentoEnergeticoAPI.Tests;

public class ConsumoEnergiaControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ConsumoEnergiaControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/consumoenergia");

        response.EnsureSuccessStatusCode();
    }
}
