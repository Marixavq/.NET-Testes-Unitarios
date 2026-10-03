namespace Triangulos.Core;

/// <summary>Angulos internos, em graus. A e oposto ao lado a, B ao lado b e C ao lado c.</summary>
public sealed record Angulos(double A, double B, double C);

/// <summary>Resultado completo da analise de um triangulo.</summary>
public sealed record AnaliseTriangulo(
    TipoTriangulo Tipo,
    TipoPorAngulo TipoPorAngulo,
    double Perimetro,
    double Area,
    Angulos Angulos);
