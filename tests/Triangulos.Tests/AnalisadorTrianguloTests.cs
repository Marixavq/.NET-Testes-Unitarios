using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class AnalisadorTrianguloTests
{
    // RN03 a RN08
    [Theory]
    [InlineData(3.0, 4.0, 5.0, TipoTriangulo.Escaleno, TipoPorAngulo.Retangulo, 12.0, 6.0, 36.87, 53.13, 90.0)]
    [InlineData(5.0, 3.0, 4.0, TipoTriangulo.Escaleno, TipoPorAngulo.Retangulo, 12.0, 6.0, 90.0, 36.87, 53.13)]
    [InlineData(2.0, 3.0, 4.0, TipoTriangulo.Escaleno, TipoPorAngulo.Obtusangulo, 9.0, 2.9, 28.96, 46.57, 104.48)]
    [InlineData(3.0, 3.0, 4.0, TipoTriangulo.Isosceles, TipoPorAngulo.Acutangulo, 10.0, 4.47, 48.19, 48.19, 83.62)]
    [InlineData(2.0, 2.0, 2.0, TipoTriangulo.Equilatero, TipoPorAngulo.Acutangulo, 6.0, 1.73, 60.0, 60.0, 60.0)]
    public void Analisar_LadosValidos_RetornaAnaliseCompleta(
        double a, double b, double c,
        TipoTriangulo tipo, TipoPorAngulo tipoPorAngulo, double perimetro, double area,
        double anguloA, double anguloB, double anguloC)
    {
        // Arrange
        var esperado = new AnaliseTriangulo(tipo, tipoPorAngulo, perimetro, area,
            new Angulos(anguloA, anguloB, anguloC));

        // Act
        var analise = AnalisadorTriangulo.Analisar(a, b, c);

        // Assert
        Assert.Equal(esperado, analise);
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void Analisar_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => AnalisadorTriangulo.Analisar(a, b, c));
    }
}
