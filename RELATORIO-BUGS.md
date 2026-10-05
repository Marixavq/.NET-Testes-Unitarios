# Relatorio de Bugs

**Aluno(s):** Júlia Tiziotto Buttler, 564975 -
Mariana Xavier Quispe, 566357 **Turma:** 2TDSA  **Data:** 04/10/2026

## 1. Resumo

| Item | Valor |
|------|-------|
| Total de casos de teste escritos | 210 |
| Casos vermelhos no codigo original ("antes") | 95 |
| Casos vermelhos no codigo corrigido ("depois") | 0 |
| Cobertura de linhas (Triangulos.Core) | 100% |
| Cobertura de ramos (Triangulos.Core) | 100% (38 de 38) |

Suite vermelha: commit `014eb7d`. Evidencias em `evidencias/antes/`, `evidencias/depois/`
(com `relatorio/Summary.txt`) e `evidencias/sensibilidade/`. Exploracao caixa-preta da Fase 1
em `evidencias/fase1-caixa-preta.md`.

## 2. Bugs encontrados

| ID | Regra violada (RN) | Sintoma (entrada -> obtido x esperado) | Local (arquivo:metodo) | Causa raiz | Correcao aplicada | Teste que prova (nome) | Commit |
|----|--------------------|----------------------------------------|------------------------|------------|-------------------|------------------------|--------|
| B01 | RN03 | `Classificar(3, 4, 3)` -> Escaleno x Isosceles | `ClassificadorTriangulo.cs:Classificar` | A condicao do isosceles comparava so `a == b` e `b == c`, esquecendo o par `a == c` | Adicionado `\|\| a == c` na condicao | `Classificar_DoisLadosIguais_RetornaIsosceles(3, 4, 3)` | `eb6849e` |
| B02 | RN02 | `Classificar(4, 1, 2)` -> Escaleno x NaoEhTriangulo | `ClassificadorTriangulo.cs:EhTriangulo` | Apenas duas das tres desigualdades eram verificadas; faltava a do lado `a` contra `b + c` | Adicionado `if (b + c < a) return false;` | `Classificar_LadosNaoFormamTriangulo_RetornaNaoEhTriangulo(4, 1, 2)` | `35aae1f` |
| B03 | RN02 (e RN09/RN10 por consequencia) | `Classificar(1, 2, 3)` -> Escaleno x NaoEhTriangulo; `POST /analisar` 1,2,3 -> 200 x 422 | `ClassificadorTriangulo.cs:EhTriangulo` | Comparacoes estritas (`a + b < c`) aceitavam a igualdade, ou seja, o triangulo degenerado | `<` trocado por `<=` nas tres desigualdades | `EhTriangulo_LadosNaoFormamTriangulo_RetornaFalse(1, 2, 3)` | `5f47443` |
| B04 | RN01 | `Classificar(NaN, 4, 5)` -> Escaleno x NaoEhTriangulo | `ClassificadorTriangulo.cs:EhTriangulo` | So havia a verificacao `<= 0`. Toda comparacao com NaN e falsa, entao nenhuma regra o rejeitava | Adicionado `!double.IsFinite(...)` para cada lado | `EhTriangulo_LadoInvalido_RetornaFalse(NaN, 3, 4)` | `3b23384` |
| B05 | RN05 | `Area(4, 5, 6)` -> 9.9 x 9.92 | `CalculadoraTriangulo.cs:Area` | `Math.Round` com 1 casa decimal | Arredondamento com 2 casas (mantido `AwayFromZero`) | `Area_LadosValidos_RetornaAreaArredondada(4, 5, 6)` | `5582c10` |
| B06 | RN06 | `Angulos(2, 3, 4).C` -> 100.81 x 104.48 | `CalculadoraTriangulo.cs:Angulos` | O cosseno de gama usava o denominador `2ac` (copiado da linha de beta) em vez de `2ab` | Denominador trocado para `2 * a * b` | `Angulos_LadosValidos_RetornaAngulosOpostosArredondados(2, 3, 4)` | `0fdb8e6` |
| B07 | RN07 / RN08 | `ClassificarPorAngulo(5, 3, 4)` -> Acutangulo x Retangulo | `CalculadoraTriangulo.cs:EhRetangulo` | Supunha que a hipotenusa era sempre `c` | Lados ordenados; `z` passa a ser o maior lado | `EhRetangulo_DentroDaTolerancia_RetornaTrue(5, 3, 4)` | `f733338` |
| B08 | RN07 | `EhRetangulo(3, 4, 5.0000000001)` -> false x true | `CalculadoraTriangulo.cs:EhRetangulo` | Igualdade exata (`==`) em vez da tolerancia relativa da especificacao | `Math.Abs(x*x + y*y - z*z) <= 1e-9 * z*z` | `EhRetangulo_DentroDaTolerancia_RetornaTrue(3, 4, 5.0000000001)` | `f61725c` |
| B09 | RN10 | `GET /classificar?a=3&b=4` -> 200 NaoEhTriangulo x 400 | `Program.cs:MapGet("/classificar")` | O parametro `c` tinha valor padrao `= 0`, entao a ausencia virava um lado zero | Removido o valor padrao; a Minimal API passa a exigir `c` | `Classificar_ParametroAusente_Retorna400("a=3&b=4")` | `2385eef` |

Observacoes:
- B01 a B04 estavam no mesmo metodo e se mascaravam: corrigir B02 ja fez Infinity no lado `a` ser rejeitado,
  e B03 fez o caso (Infinity, Infinity, Infinity) ser rejeitado. B04 ficou restrito ao NaN, mas a correcao
  usa `IsFinite` para que a RN01 fique explicita e nao dependa das desigualdades.
- B02 e B03 moram nas mesmas linhas (as desigualdades da RN02), mas sao defeitos distintos e foram corrigidos
  em commits separados.
- B06 nao aparecia no 3-4-5 (numerador zero da 90 graus com qualquer denominador) nem no equilatero (`2ac == 2ab`).
- B04 e B08 so aparecem com entradas especiais de `double` (NaN e diferencas de ponto flutuante).

## 3. Prova de sensibilidade (pelo menos 3 bugs)

A reintroducao foi feita editando o arquivo corrigido (equivalente ao `git checkout <hash> -- arquivo` do
enunciado), rodando o filtro de testes e restaurando a correcao em seguida. Apos restaurar, a suite inteira
voltou a 210/210 verde.

| Bug | Como reintroduzi | Teste que falhou | Evidencia (arquivo em evidencias/sensibilidade/) |
|-----|------------------|------------------|--------------------------------------------------|
| B01 | `if (a == b \|\| b == c \|\| a == c)` voltou a ser `if (a == b \|\| b == c)` | `Classificar_DoisLadosIguais_RetornaIsosceles` (2 casos) | `B01.txt` |
| B03 | As tres comparacoes `<=` de `EhTriangulo` voltaram a ser `<` | `EhTriangulo_LadosNaoFormamTriangulo_RetornaFalse` e `Classificar_LadosNaoFormamTriangulo_RetornaNaoEhTriangulo` (18 casos) | `B03.txt` |
| B06 | Denominador do angulo C voltou de `2 * a * b` para `2 * a * c` | `Angulos_LadosValidos_RetornaAngulosOpostosArredondados`, `Angulos_TrianguloValido_SomaEh180Graus`, `Angulos_LadosRotacionados_AngulosRotacionamJunto` (14 casos) | `B06.txt` |

## 4. Bugs que eu NAO consegui corrigir / duvidas

Nenhum. Todos os 9 defeitos encontrados foram corrigidos e a suite termina 210/210 verde.

## 5. O que aprendi

Aprendi que testar a partir da especificação é importante porque ajuda a encontrar bugs que não são tão fáceis de perceber olhando só o código. Um exemplo foi a desigualdade que estava faltando em EhTriangulo, que provavelmente passaria despercebida apenas lendo a implementação. Também percebi que testar só casos “óbvios”, como o triângulo 3-4-5, não é suficiente, já que o erro no ângulo C só aparecia em outros casos. Então aprendi que é importante testar valores-limite, todas as permutações dos lados e também valores especiais de double, como NaN e infinito.

Outra coisa que aprendi é que alguns bugs podem acabar escondendo outros e que é melhor corrigir um problema de cada vez e fazer um commit separado para cada defeito e rodar todos os testes depois de cada alteração, assim fica mais fácil entender o que cada correção realmente mudou.
