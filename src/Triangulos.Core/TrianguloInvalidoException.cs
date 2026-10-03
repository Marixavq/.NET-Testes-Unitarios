using System.Globalization;

namespace Triangulos.Core;

/// <summary>Lancada quando os lados informados nao formam um triangulo.</summary>
public sealed class TrianguloInvalidoException : Exception
{
    public TrianguloInvalidoException(double a, double b, double c)
        : base(string.Create(CultureInfo.InvariantCulture,
            $"Os lados {a}, {b} e {c} nao formam um triangulo valido."))
    {
    }
}
