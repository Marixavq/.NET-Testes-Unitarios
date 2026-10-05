# Fase 1 - Exploracao caixa-preta

Exploracao feita apenas pela API (`dotnet run --project src/Triangulos.Api --urls http://localhost:5000`),
sem abrir `src/Triangulos.Core`. Os valores esperados foram calculados a partir da especificacao
(secao 4 do enunciado), nunca a partir das respostas da API.

## Tabela de suspeitas

| # | Entrada | Obtido | Esperado | RN |
|---|---------|--------|----------|----|
| S1 | GET 3,4,3 | Escaleno | Isosceles | RN03 |
| S2 | GET 1,2,3 / 3,1,2 / 2,3,1 / 1,1,2 / 2,2,4 | Escaleno / Escaleno / Escaleno / Isosceles / Isosceles | NaoEhTriangulo | RN02 |
| S3 | GET 4,1,2 (1,2,4 responde certo) | Escaleno | NaoEhTriangulo | RN02 |
| S4 | GET NaN,4,5 / 3,NaN,5 / Infinity,4,5 / Infinity,Infinity,Infinity | Escaleno / Escaleno / Escaleno / Equilatero | NaoEhTriangulo | RN01 |
| S5 | GET a=3&b=4 (sem `c`) | 200 NaoEhTriangulo | 400 | RN10 |
| S6 | POST 5,3,4 / 4,5,3 | tipoPorAngulo Acutangulo | Retangulo | RN07 / RN08 |
| S7 | POST 2,3,4 / 4,5,6 / 3,3,4 / 5,3,4 / 4,5,3 / 4,2,3 / 3,4,2 / 2e-5,3e-5,4e-5 | angulo c = 100.81 / 84.02 / 85.22 / 63.26 / 0 / 62.72 / 0 / 100.81 | 104.48 / 82.82 / 83.62 / 53.13 / 36.87 / 46.57 / 28.96 / 104.48 | RN06 |
| S8 | POST 4,5,6 / 3,3,4 / 2,2,2 / 0.3,0.4,0.5 / 5,5.25,7.25 | area 9.9 / 4.5 / 1.7 / 0.1 / 13.1 | 9.92 / 4.47 / 1.73 / 0.06 / 13.13 | RN05 |
| S9 | POST 1,2,3 | 200 com analise | 422 com `erro` | RN09 |

Encontrado depois, na Fase 2 (teste unitario, nao pela API):

| # | Entrada | Obtido | Esperado | RN |
|---|---------|--------|----------|----|
| S10 | `EhRetangulo(3, 4, 5.0000000001)` | false | true: diferenca ~1e-9, tolerancia 1e-9 * 25 = 2.5e-8 | RN07 |

## Observacoes

- S1: o isosceles so falha quando os lados iguais sao `a` e `c`.
- S2 e S3: parecem duas falhas distintas na existencia. Uma aceita a igualdade (degenerado);
  a outra parece nao verificar `a < b + c` (4,1,2 passa, 1,2,4 nao).
- S4: zero e negativos sao rejeitados; valores nao finitos passam.
- S6: o retangulo so e reconhecido com a hipotenusa em `c`. Ja o obtusangulo com o maior lado
  em `a` ou `b` (4,2,3 e 3,4,2) e classificado corretamente.
- S7: os angulos `a` e `b` estao corretos; so o `c` erra. 3-4-5 e o equilatero acertam por coincidencia.
- S8: a area parece arredondada para 1 casa decimal em vez de 2. O caso 5,5.25,7.25 (area exata 13.125)
  so vai testar o `MidpointRounding.AwayFromZero` depois que S8 for corrigido: esperado 13.13, nao 13.12.
- S9: provavelmente consequencia de S2 (o `Analisar` nao lanca a excecao). Confirmar na Fase 3.

## Entradas que responderam conforme a especificacao

- GET 3,4,5 (Escaleno), 2,2,2 (Equilatero), 3,3,4 e 4,3,3 (Isosceles), 1,2,4 (NaoEhTriangulo),
  2,2,3.9999 (Isosceles, limite valido da RN02), 0 ou negativo em qualquer posicao (NaoEhTriangulo),
  1e308 x3 (Equilatero), 0.3,0.4,0.5 (Escaleno).
- GET sem `b` ou com `a=abc`: 400.
- POST 3,4,5 e 3000,4000,5000: resposta completa correta.
- POST 4,2,3 e 3,4,2: tipoPorAngulo Obtusangulo e angulos `a` e `b` corretos (so o `c` erra, ver S7).
- POST 3e10,4e10,5e10: Retangulo, area 6E+20. POST 2e-5,3e-5,4e-5: Obtusangulo, area 0 (2.9e-10 arredondado).
  A tolerancia relativa da RN07 parece correta nas duas escalas.
- POST 0,4,5 e -3,4,5: 422 com `erro`. POST com JSON malformado: 400.
