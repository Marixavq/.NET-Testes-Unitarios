using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Triangulos.Tests.Api;

public class TriangulosApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TriangulosApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    // RN10
    [Fact]
    public async Task Health_Get_RetornaStatusOk()
    {
        // Act
        var resposta = await _client.GetAsync("/health");
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal("ok", json.GetProperty("status").GetString());
    }

    // RN10
    [Theory]
    [InlineData("a=3&b=4&c=5", "Escaleno")]
    [InlineData("a=2&b=2&c=2", "Equilatero")]
    [InlineData("a=3&b=4&c=3", "Isosceles")]
    public async Task Classificar_LadosValidos_Retorna200ComTipo(string query, string tipoEsperado)
    {
        // Act
        var resposta = await _client.GetAsync($"/api/triangulos/classificar?{query}");
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        Assert.Equal(tipoEsperado, json.GetProperty("tipo").GetString());
    }

    // RN10: triangulo inexistente nao e erro
    [Theory]
    [InlineData("a=1&b=2&c=3")]
    [InlineData("a=0&b=4&c=5")]
    [InlineData("a=-3&b=4&c=5")]
    [InlineData("a=NaN&b=4&c=5")]
    public async Task Classificar_LadosQueNaoFormamTriangulo_Retorna200ComNaoEhTriangulo(string query)
    {
        // Act
        var resposta = await _client.GetAsync($"/api/triangulos/classificar?{query}");
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        Assert.Equal("NaoEhTriangulo", json.GetProperty("tipo").GetString());
    }

    // RN10
    [Theory]
    [InlineData("a=3&b=4")]
    [InlineData("a=3&c=5")]
    [InlineData("b=4&c=5")]
    [InlineData("")]
    public async Task Classificar_ParametroAusente_Retorna400(string query)
    {
        // Act
        var resposta = await _client.GetAsync($"/api/triangulos/classificar?{query}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
    }

    // RN10
    [Theory]
    [InlineData("a=abc&b=4&c=5")]
    [InlineData("a=3&b=4&c=xyz")]
    public async Task Classificar_ParametroNaoNumerico_Retorna400(string query)
    {
        // Act
        var resposta = await _client.GetAsync($"/api/triangulos/classificar?{query}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
    }

    // RN10: resposta da secao 5 do enunciado
    [Fact]
    public async Task Analisar_Triangulo345_Retorna200ComAnaliseCompleta()
    {
        // Act
        var resposta = await _client.PostAsJsonAsync("/api/triangulos/analisar", new { a = 3, b = 4, c = 5 });
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        Assert.Equal("Escaleno", json.GetProperty("tipo").GetString());
        Assert.Equal("Retangulo", json.GetProperty("tipoPorAngulo").GetString());
        Assert.Equal(12.0, json.GetProperty("perimetro").GetDouble());
        Assert.Equal(6.0, json.GetProperty("area").GetDouble());
        var angulos = json.GetProperty("angulos");
        Assert.Equal(36.87, angulos.GetProperty("a").GetDouble());
        Assert.Equal(53.13, angulos.GetProperty("b").GetDouble());
        Assert.Equal(90.0, angulos.GetProperty("c").GetDouble());
    }

    // RN10
    [Theory]
    [InlineData(1.0, 2.0, 3.0)]
    [InlineData(4.0, 1.0, 2.0)]
    [InlineData(0.0, 4.0, 5.0)]
    [InlineData(-3.0, 4.0, 5.0)]
    public async Task Analisar_LadosQueNaoFormamTriangulo_Retorna422ComErro(double a, double b, double c)
    {
        // Act
        var resposta = await _client.PostAsJsonAsync("/api/triangulos/analisar", new { a, b, c });
        var json = await resposta.Content.ReadFromJsonAsync<JsonElement>();

        // Assert
        Assert.Equal(HttpStatusCode.UnprocessableEntity, resposta.StatusCode);
        Assert.False(string.IsNullOrWhiteSpace(json.GetProperty("erro").GetString()));
    }

    // RN10
    [Theory]
    [InlineData("{\"a\":3,")]
    [InlineData("nao e json")]
    [InlineData("")]
    public async Task Analisar_JsonMalformado_Retorna400(string corpo)
    {
        // Arrange
        var conteudo = new StringContent(corpo, Encoding.UTF8, "application/json");

        // Act
        var resposta = await _client.PostAsync("/api/triangulos/analisar", conteudo);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
    }
}
