using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Triangulos.Tests.Api;

/// <summary>
/// EXEMPLO de teste de integracao: sobe a Minimal API em memoria e faz chamadas HTTP reais.
/// </summary>
public class ExemploApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ExemploApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task Health_RetornaOk()
    {
        var resposta = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
    }

    [Fact]
    public async Task Classificar_Triangulo345_RetornaEscaleno()
    {
        var resposta = await _client.GetAsync("/api/triangulos/classificar?a=3&b=4&c=5");
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        Assert.Equal("Escaleno", json.GetProperty("tipo").GetString());
    }

    // TODO: teste tambem o POST /api/triangulos/analisar e os codigos 400 e 422.
    // Dica: PostAsJsonAsync("/api/triangulos/analisar", new { a = 3, b = 4, c = 5 })
}
