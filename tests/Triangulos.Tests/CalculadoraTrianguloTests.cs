using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class CalculadoraTrianguloTests
{
    // RN04
    [Theory]
    [InlineData(3.0, 4.0, 5.0, 12.0)]
    [InlineData(1.0, 1.0, 1.0, 3.0)]
    [InlineData(2.0, 3.0, 4.0, 9.0)]
    [InlineData(0.5, 0.5, 0.5, 1.5)]
    [InlineData(3000.0, 4000.0, 5000.0, 12000.0)]
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
    [InlineData(5.0, 3.0, 4.0, 6.00)]
    [InlineData(2.0, 2.0, 2.0, 1.73)]
    [InlineData(2.0, 3.0, 4.0, 2.90)]
    [InlineData(4.0, 5.0, 6.0, 9.92)]
    [InlineData(3.0, 3.0, 4.0, 4.47)]
    [InlineData(0.3, 0.4, 0.5, 0.06)]
    public void Area_LadosValidos_RetornaAreaArredondada(
        double a, double b, double c, double esperado)
    {
        // Act
        var area = CalculadoraTriangulo.Area(a, b, c);

        // Assert
        Assert.Equal(esperado, area);
    }

    // RN05: a area exata e 13.125 (valor exato em double)
    [Fact]
    public void Area_TerceiraCasaExatamenteNoMeio_ArredondaAwayFromZero()
    {
        // Act
        var area = CalculadoraTriangulo.Area(5.0, 5.25, 7.25);

        // Assert
        Assert.Equal(13.13, area);
    }

    // RN06
    [Theory]
    [InlineData(3.0, 4.0, 5.0, 36.87, 53.13, 90.0)]
    [InlineData(5.0, 3.0, 4.0, 90.0, 36.87, 53.13)]
    [InlineData(4.0, 5.0, 3.0, 53.13, 90.0, 36.87)]
    [InlineData(2.0, 3.0, 4.0, 28.96, 46.57, 104.48)]
    [InlineData(4.0, 2.0, 3.0, 104.48, 28.96, 46.57)]
    [InlineData(3.0, 4.0, 2.0, 46.57, 104.48, 28.96)]
    [InlineData(4.0, 5.0, 6.0, 41.41, 55.77, 82.82)]
    [InlineData(3.0, 3.0, 4.0, 48.19, 48.19, 83.62)]
    [InlineData(2.0, 2.0, 2.0, 60.0, 60.0, 60.0)]
    public void Angulos_LadosValidos_RetornaAngulosOpostosArredondados(
        double a, double b, double c, double anguloA, double anguloB, double anguloC)
    {
        // Act
        var angulos = CalculadoraTriangulo.Angulos(a, b, c);

        // Assert
        Assert.Equal(new Angulos(anguloA, anguloB, anguloC), angulos);
    }

    // RN07
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(5.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 3.0)]
    [InlineData(13.0, 5.0, 12.0)]
    [InlineData(0.3, 0.4, 0.5)]
    [InlineData(3000.0, 4000.0, 5000.0)]
    [InlineData(3e10, 4e10, 5e10)]
    [InlineData(3.0, 4.0, 5.0000000001)]
    public void EhRetangulo_DentroDaTolerancia_RetornaTrue(double a, double b, double c)
    {
        // Act
        var ehRetangulo = CalculadoraTriangulo.EhRetangulo(a, b, c);

        // Assert
        Assert.True(ehRetangulo);
    }

    // RN07
    [Theory]
    [InlineData(2.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 6.0)]
    [InlineData(2.0, 2.0, 2.0)]
    [InlineData(2e-5, 3e-5, 4e-5)]
    [InlineData(3.0, 4.0, 5.000001)]
    [InlineData(5.000001, 3.0, 4.0)]
    public void EhRetangulo_ForaDaTolerancia_RetornaFalse(double a, double b, double c)
    {
        // Act
        var ehRetangulo = CalculadoraTriangulo.EhRetangulo(a, b, c);

        // Assert
        Assert.False(ehRetangulo);
    }

    // RN08
    [Theory]
    [InlineData(3.0, 4.0, 5.0)]
    [InlineData(5.0, 3.0, 4.0)]
    [InlineData(4.0, 5.0, 3.0)]
    public void ClassificarPorAngulo_TrianguloRetangulo_RetornaRetangulo(double a, double b, double c)
    {
        // Act
        var tipo = CalculadoraTriangulo.ClassificarPorAngulo(a, b, c);

        // Assert
        Assert.Equal(TipoPorAngulo.Retangulo, tipo);
    }

    // RN08
    [Theory]
    [InlineData(2.0, 3.0, 4.0)]
    [InlineData(4.0, 2.0, 3.0)]
    [InlineData(3.0, 4.0, 2.0)]
    [InlineData(2e-5, 3e-5, 4e-5)]
    public void ClassificarPorAngulo_MaiorLadoAoQuadradoMaiorQueSoma_RetornaObtusangulo(
        double a, double b, double c)
    {
        // Act
        var tipo = CalculadoraTriangulo.ClassificarPorAngulo(a, b, c);

        // Assert
        Assert.Equal(TipoPorAngulo.Obtusangulo, tipo);
    }

    // RN08
    [Theory]
    [InlineData(4.0, 5.0, 6.0)]
    [InlineData(6.0, 4.0, 5.0)]
    [InlineData(2.0, 2.0, 2.0)]
    [InlineData(3.0, 3.0, 4.0)]
    public void ClassificarPorAngulo_MaiorLadoAoQuadradoMenorQueSoma_RetornaAcutangulo(
        double a, double b, double c)
    {
        // Act
        var tipo = CalculadoraTriangulo.ClassificarPorAngulo(a, b, c);

        // Assert
        Assert.Equal(TipoPorAngulo.Acutangulo, tipo);
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void Perimetro_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => CalculadoraTriangulo.Perimetro(a, b, c));
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void Area_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => CalculadoraTriangulo.Area(a, b, c));
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void Angulos_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => CalculadoraTriangulo.Angulos(a, b, c));
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void EhRetangulo_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => CalculadoraTriangulo.EhRetangulo(a, b, c));
    }

    // RN09
    [Theory]
    [MemberData(nameof(DadosTriangulo.LadosInvalidosParaCalculo), MemberType = typeof(DadosTriangulo))]
    public void ClassificarPorAngulo_LadosInvalidos_LancaTrianguloInvalidoException(double a, double b, double c)
    {
        // Act & Assert
        Assert.Throws<TrianguloInvalidoException>(() => CalculadoraTriangulo.ClassificarPorAngulo(a, b, c));
    }
}
