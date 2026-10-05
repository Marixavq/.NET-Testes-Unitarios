using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class ClassificadorTrianguloTests
{
    // RN01 / RN02
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(2.0, 2.0, 2.0)]
    [InlineData(3.0, 3.0, 4.0)]
    [InlineData(2.0, 2.0, 3.9999)]
    [InlineData(0.3, 0.4, 0.5)]
    [InlineData(1e308, 1e308, 1e308)]
    public void EhTriangulo_LadosValidos_RetornaTrue(double a, double b, double c)
    {
        // Act
        var ehTriangulo = ClassificadorTriangulo.EhTriangulo(a, b, c);

        // Assert
        Assert.True(ehTriangulo);
    }

    // RN01
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidos), MemberType = typeof(DadosTriangulo))]
    public void EhTriangulo_LadoInvalido_RetornaFalse(double a, double b, double c)
    {
        // Act
        var ehTriangulo = ClassificadorTriangulo.EhTriangulo(a, b, c);

        // Assert
        Assert.False(ehTriangulo);
    }

    // RN02
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosQueNaoFormamTriangulo), MemberType = typeof(DadosTriangulo))]
    public void EhTriangulo_LadosNaoFormamTriangulo_RetornaFalse(double a, double b, double c)
    {
        // Act
        var ehTriangulo = ClassificadorTriangulo.EhTriangulo(a, b, c);

        // Assert
        Assert.False(ehTriangulo);
    }

    //RN01
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidos), MemberType = typeof(DadosTriangulo))]
    public void Classificar_LadoInvalido_RetornaNaoEhTriangulo(
        double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.NaoEhTriangulo, tipo);
    }

    // RN01 
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(1.0, 1.0, 1.0)]
    public void Classificar_LadosPositivosEFinitos_NaoRetornaNaoEhTriangulo(
        double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.NotEqual(TipoTriangulo.NaoEhTriangulo, tipo);
    }

    // RN02
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosQueNaoFormamTriangulo), MemberType = typeof(DadosTriangulo))]
    public void Classificar_LadosNaoFormamTriangulo_RetornaNaoEhTriangulo(
        double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.NaoEhTriangulo, tipo);
    }

    // RN03
    [Theory]
    [InlineData(0.5, 0.5, 0.5)]
    [InlineData(1000.0, 1000.0, 1000.0)]
    [InlineData(double.Epsilon, double.Epsilon, double.Epsilon)]
    [InlineData(double.MaxValue, double.MaxValue, double.MaxValue)]
    public void Classificar_TresLadosIguais_RetornaEquilatero(double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.Equilatero, tipo);
    }

    // RN03
    [Theory]
    [InlineData(3.0, 3.0, 4.0)]
    [InlineData(3.0, 4.0, 3.0)]
    [InlineData(4.0, 3.0, 3.0)]
    [InlineData(2.0, 2.0, 3.9999)]
    [InlineData(0.5, 0.7, 0.5)]
    public void Classificar_DoisLadosIguais_RetornaIsosceles(double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.Isosceles, tipo);
    }

    // RN03
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(5.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 3.0)]
    [InlineData(2.0, 3.0, 4.0)]
    [InlineData(0.3, 0.4, 0.5)]
    public void Classificar_TresLadosDiferentes_RetornaEscaleno(double a, double b, double c)
    {
        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.Escaleno, tipo);
    }
}
