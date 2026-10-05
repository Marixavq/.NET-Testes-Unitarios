using Xunit;

namespace Triangulos.Tests;

public static class DadosTriangulo
{
    // RN01: lado nao finito ou nao estritamente positivo, em cada posicao
    public static TheoryData<double, double, double> LadosInvalidos => new()
    {
        { 0.0, 3.0, 4.0 },
        { 3.0, 0.0, 4.0 },
        { 3.0, 4.0, 0.0 },
        { -1.0, 3.0, 4.0 },
        { 3.0, -1.0, 4.0 },
        { 3.0, 4.0, -1.0 },
        { double.NaN, 3.0, 4.0 },
        { 3.0, double.NaN, 4.0 },
        { 3.0, 4.0, double.NaN },
        { double.PositiveInfinity, 3.0, 4.0 },
        { 3.0, double.PositiveInfinity, 4.0 },
        { 3.0, 4.0, double.PositiveInfinity },
        { double.NegativeInfinity, 3.0, 4.0 },
        { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
    };

    // RN02: degenerados (igualdade) e soma menor que o terceiro lado, em todas as posicoes
    public static TheoryData<double, double, double> LadosQueNaoFormamTriangulo => new()
    {
        { 1.0, 2.0, 3.0 },
        { 1.0, 3.0, 2.0 },
        { 2.0, 1.0, 3.0 },
        { 2.0, 3.0, 1.0 },
        { 3.0, 1.0, 2.0 },
        { 3.0, 2.0, 1.0 },
        { 1.0, 1.0, 2.0 },
        { 1.0, 2.0, 1.0 },
        { 2.0, 1.0, 1.0 },
        { 1.0, 2.0, 4.0 },
        { 1.0, 4.0, 2.0 },
        { 4.0, 1.0, 2.0 },
    };

    // RN09: amostra de RN01 e RN02 para os metodos que devem lancar
    public static TheoryData<double, double, double> LadosInvalidosParaCalculo => new()
    {
        { 0.0, 4.0, 5.0 },
        { -3.0, 4.0, 5.0 },
        { double.NaN, 4.0, 5.0 },
        { double.PositiveInfinity, 4.0, 5.0 },
        { 1.0, 2.0, 3.0 },
        { 4.0, 1.0, 2.0 },
    };
}
