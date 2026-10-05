using System.Text.Json.Serialization;
using Triangulos.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

var api = app.MapGroup("/api/triangulos");

api.MapGet("/classificar", (double a, double b, double c) =>
    Results.Ok(new { tipo = ClassificadorTriangulo.Classificar(a, b, c) }));

api.MapPost("/analisar", (LadosRequest lados) =>
{
    try
    {
        return Results.Ok(AnalisadorTriangulo.Analisar(lados.A, lados.B, lados.C));
    }
    catch (TrianguloInvalidoException ex)
    {
        return Results.UnprocessableEntity(new { erro = ex.Message });
    }
});

app.Run();

public sealed record LadosRequest(double A, double B, double C);

// Necessario para que os testes de integracao (WebApplicationFactory<Program>) enxerguem a API.
public partial class Program;
