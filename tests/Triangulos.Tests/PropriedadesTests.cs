using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class PropriedadesTests
{
    private static IEnumerable<(double A, double B, double C)> Permutacoes(double a, double b, double c) =>
        new[] { (a, b, c), (a, c, b), (b, a, c), (b, c, a), (c, a, b), (c, b, a) };

    // RN03: o resultado nao depende da ordem dos lados
    [Theory]
    [InlineData(3.0, 4.0, 5.0, TipoTriangulo.Escaleno)]
    [InlineData(3.0, 3.0, 4.0, TipoTriangulo.Isosceles)]
    [InlineData(2.0, 2.0, 2.0, TipoTriangulo.Equilatero)]
    [InlineData(1.0, 2.0, 3.0, TipoTriangulo.NaoEhTriangulo)]
    [InlineData(1.0, 2.0, 4.0, TipoTriangulo.NaoEhTriangulo)]
    public void Classificar_QualquerOrdemDosLados_RetornaMesmoTipo(
        double a, double b, double c, TipoTriangulo esperado)
    {
        // Act
        var tipos = Permutacoes(a, b, c).Select(p => ClassificadorTriangulo.Classificar(p.A, p.B, p.C));

        // Assert
        Assert.All(tipos, tipo => Assert.Equal(esperado, tipo));
    }

    // RN07 / RN08: a hipotenusa (ou o maior lado) pode estar em qualquer posicao
    [Theory]
    [InlineData(3.0, 4.0, 5.0, TipoPorAngulo.Retangulo)]
    [InlineData(2.0, 3.0, 4.0, TipoPorAngulo.Obtusangulo)]
    [InlineData(4.0, 5.0, 6.0, TipoPorAngulo.Acutangulo)]
    public void ClassificarPorAngulo_QualquerOrdemDosLados_RetornaMesmoTipo(
        double a, double b, double c, TipoPorAngulo esperado)
    {
        // Act
        var tipos = Permutacoes(a, b, c).Select(p => CalculadoraTriangulo.ClassificarPorAngulo(p.A, p.B, p.C));

        // Assert
        Assert.All(tipos, tipo => Assert.Equal(esperado, tipo));
    }

    // RN06: cada angulo acompanha o seu lado oposto
    [Theory]
    [InlineData(2.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 6.0)]
    [InlineData(3.0, 4.0, 5.0)]
    public void Angulos_LadosRotacionados_AngulosRotacionamJunto(double a, double b, double c)
    {
        // Arrange
        var original = CalculadoraTriangulo.Angulos(a, b, c);

        // Act
        var rotacionado = CalculadoraTriangulo.Angulos(b, c, a);

        // Assert
        Assert.Equal(new Angulos(original.B, original.C, original.A), rotacionado);
    }

    // RN06: a soma dos angulos e 180 graus, a menos do arredondamento
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(2.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 6.0)]
    [InlineData(3.0, 3.0, 4.0)]
    [InlineData(7.0, 8.0, 9.0)]
    [InlineData(0.3, 0.4, 0.5)]
    public void Angulos_TrianguloValido_SomaEh180Graus(double a, double b, double c)
    {
        // Act
        var angulos = CalculadoraTriangulo.Angulos(a, b, c);

        // Assert
        Assert.InRange(angulos.A + angulos.B + angulos.C, 179.97, 180.03);
    }

    // RN06: multiplicar todos os lados por k nao altera os angulos
    [Theory]
    [InlineData(2.0, 3.0, 4.0, 10.0)]
    [InlineData(2.0, 3.0, 4.0, 0.1)]
    [InlineData(4.0, 5.0, 6.0, 1000.0)]
    [InlineData(3.0, 4.0, 5.0, 3.0)]
    public void Angulos_LadosMultiplicadosPorK_MantemAngulos(double a, double b, double c, double k)
    {
        // Arrange
        var original = CalculadoraTriangulo.Angulos(a, b, c);

        // Act
        var escalado = CalculadoraTriangulo.Angulos(a * k, b * k, c * k);

        // Assert
        Assert.Equal(original, escalado);
    }
}
