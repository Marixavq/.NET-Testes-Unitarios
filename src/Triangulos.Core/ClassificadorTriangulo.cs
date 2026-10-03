namespace Triangulos.Core;

public static class ClassificadorTriangulo
{
    public static bool EhTriangulo(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
        {
            return false;
        }

        if (a + b < c)
        {
            return false;
        }

        if (a + c < b)
        {
            return false;
        }

        return true;
    }

    public static TipoTriangulo Classificar(double a, double b, double c)
    {
        if (!EhTriangulo(a, b, c))
        {
            return TipoTriangulo.NaoEhTriangulo;
        }

        if (a == b && b == c)
        {
            return TipoTriangulo.Equilatero;
        }

        if (a == b || b == c)
        {
            return TipoTriangulo.Isosceles;
        }

        return TipoTriangulo.Escaleno;
    }
}
