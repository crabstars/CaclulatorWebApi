using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestProject1;

public class PrimeEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PrimeEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(1, false)]
    [InlineData(-7, false)]
    [InlineData(97, true)]
    public async Task GetPrimeEndpoint_ReturnsExpectedResult(int number, bool isPrime)
    {
        var response = await _client.GetAsync($"/prime/{number}");

        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<PrimeResponse>();

        Assert.NotNull(payload);
        Assert.Equal(number, payload.Number);
        Assert.Equal(isPrime, payload.IsPrime);
    }

    private sealed record PrimeResponse(int Number, bool IsPrime);
}
