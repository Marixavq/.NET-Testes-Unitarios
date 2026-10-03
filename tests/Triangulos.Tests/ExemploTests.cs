using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

/// <summary>
/// EXEMPLO de teste unitario (padrao AAA: Arrange, Act, Assert).
/// Apague ou renomeie este arquivo conforme organizar a sua suite.
/// Nomeie os testes assim:  Metodo_Cenario_ResultadoEsperado
/// </summary>
public class ExemploTests
{
    [Fact]
    public void Classificar_TresLadosDiferentes_RetornaEscaleno()
    {
        // Arrange
        double a = 3, b = 4, c = 5;

        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.Escaleno, tipo);
    }

    // Um [Theory] executa o mesmo teste para varios conjuntos de dados.
    // Use valores com ponto decimal (2.0, nao 2) para evitar conversoes implicitas.
    [Theory]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(2.5, 2.5, 2.5)]
    public void Classificar_TresLadosIguais_RetornaEquilatero(double a, double b, double c)
    {
        Assert.Equal(TipoTriangulo.Equilatero, ClassificadorTriangulo.Classificar(a, b, c));
    }

    // TODO: escreva o restante da suite a partir da ESPECIFICACAO (enunciado, secao 4),
    //       nunca a partir do que o codigo faz.
}
