namespace Triangulos.Core;

public static class AnalisadorTriangulo
{
    public static AnaliseTriangulo Analisar(double a, double b, double c)
    {
        if (!ClassificadorTriangulo.EhTriangulo(a, b, c))
        {
            throw new TrianguloInvalidoException(a, b, c);
        }

        return new AnaliseTriangulo(
            ClassificadorTriangulo.Classificar(a, b, c),
            CalculadoraTriangulo.ClassificarPorAngulo(a, b, c),
            CalculadoraTriangulo.Perimetro(a, b, c),
            CalculadoraTriangulo.Area(a, b, c),
            CalculadoraTriangulo.Angulos(a, b, c));
    }
}
