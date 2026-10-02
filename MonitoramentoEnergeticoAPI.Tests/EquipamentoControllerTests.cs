using Xunit;

namespace MonitoramentoEnergeticoAPI.Tests;

public class EquipamentoControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EquipamentoControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/equipamento");

        response.EnsureSuccessStatusCode();
    }
}
