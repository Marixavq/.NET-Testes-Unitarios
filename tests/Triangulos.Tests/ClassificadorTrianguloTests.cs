using Triangulos.Core;
using Xunit;

namespace Triangulos.Tests;

public class ClassificadorTrianguloTests
{
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
}