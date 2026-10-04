using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class ClassificadorTrianguloTests
{

    //RN01
    [Theory]
    [InlineData(0.0, 3.0, 4.0)]
    [InlineData(-1.0, 3.0, 4.0)]
    [InlineData(double.PositiveInfinity, 3.0, 4.0)]
    [InlineData(double.NegativeInfinity, 3.0, 4.0)]
    [InlineData(double.NaN, 3.0, 4.0)]
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
    [InlineData(1.0, 2.0, 3.0)]   // a + b = c
    [InlineData(1.0, 3.0, 2.0)]   // a + c = b
    [InlineData(3.0, 1.0, 2.0)]   // b + c = a
    [InlineData(1.0, 2.0, 4.0)]   // c > a + b
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
    [InlineData(3.0, 3.0, 4.0)]
    [InlineData(3.0, 4.0, 3.0)]
    [InlineData(4.0, 3.0, 3.0)]

    public void Classificar_DoisLadosIguais_RetornaIsosceles(double a, double b, double c)
    {

        // Act
        var tipo = ClassificadorTriangulo.Classificar(a, b, c);

        // Assert
        Assert.Equal(TipoTriangulo.Isosceles, tipo);
    }


    // RN04
    [Theory]
    [InlineData(3.0, 4.0, 5.0, 12.0)]
    [InlineData(1.0, 1.0, 1.0, 3.0)]
    [InlineData(2.0, 3.0, 4.0, 9.0)]
    public void Perimetro_LadosValidos_RetornaSomaDosLados(
        double a, double b, double c, double esperado)
    {
        // Act
        var perimetro = CalculadoraTriangulo.Perimetro(a, b, c);

        // Assert
        Assert.Equal(esperado, perimetro);
    }

    // RN05
    [Theory]
    [InlineData(3.0, 4.0, 5.0, 6.00)]
    [InlineData(2.0, 2.0, 2.0, 1.73)]
    [InlineData(2.0, 3.0, 4.0, 2.90)]
    public void Area_LadosValidos_RetornaAreaArredondada(
        double a, double b, double c, double esperado)
    {
        // Act
        var area = CalculadoraTriangulo.Area(a, b, c);

        // Assert
        Assert.Equal(esperado, area);
    }


}