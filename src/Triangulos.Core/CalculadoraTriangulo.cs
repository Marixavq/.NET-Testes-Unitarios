namespace Triangulos.Core;

public static class CalculadoraTriangulo
{
    public static double Perimetro(double a, double b, double c)
    {
        GarantirTriangulo(a, b, c);
        return a + b + c;
    }

    public static double Area(double a, double b, double c)
    {
        GarantirTriangulo(a, b, c);

        var s = (a + b + c) / 2;
        var area = Math.Sqrt(Math.Max(0, s * (s - a) * (s - b) * (s - c)));

        return Math.Round(area, 2, MidpointRounding.AwayFromZero);
    }

    public static Angulos Angulos(double a, double b, double c)
    {
        GarantirTriangulo(a, b, c);

        var anguloA = ParaGraus(Math.Acos(Limitar((b * b + c * c - a * a) / (2 * b * c))));
        var anguloB = ParaGraus(Math.Acos(Limitar((a * a + c * c - b * b) / (2 * a * c))));
        var anguloC = ParaGraus(Math.Acos(Limitar((a * a + b * b - c * c) / (2 * a * b))));

        return new Angulos(Arredondar(anguloA), Arredondar(anguloB), Arredondar(anguloC));
    }

    public static bool EhRetangulo(double a, double b, double c)
    {
        GarantirTriangulo(a, b, c);

        var lados = new[] { a, b, c };
        Array.Sort(lados);
        var (x, y, z) = (lados[0], lados[1], lados[2]);

        return Math.Abs(x * x + y * y - z * z) <= 1e-9 * z * z;
    }

    public static TipoPorAngulo ClassificarPorAngulo(double a, double b, double c)
    {
        GarantirTriangulo(a, b, c);

        if (EhRetangulo(a, b, c))
        {
            return TipoPorAngulo.Retangulo;
        }

        var maior = Math.Max(a, Math.Max(b, c));
        var somaQuadradosDosOutros = a * a + b * b + c * c - maior * maior;

        return maior * maior > somaQuadradosDosOutros
            ? TipoPorAngulo.Obtusangulo
            : TipoPorAngulo.Acutangulo;
    }

    private static void GarantirTriangulo(double a, double b, double c)
    {
        if (!ClassificadorTriangulo.EhTriangulo(a, b, c))
        {
            throw new TrianguloInvalidoException(a, b, c);
        }
    }

    // Erros de ponto flutuante podem levar o cosseno a 1.0000000000000002.
    private static double Limitar(double cosseno) => Math.Clamp(cosseno, -1.0, 1.0);

    private static double ParaGraus(double radianos) => radianos * 180.0 / Math.PI;

    private static double Arredondar(double valor) =>
        Math.Round(valor, 2, MidpointRounding.AwayFromZero);
}
