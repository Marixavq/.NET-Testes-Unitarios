# Exercicio: testes unitarios em .NET - O Problema do Triangulo

Voce recebeu um servico **quase** correto: ele compila, sobe e responde normalmente,
mas contem **defeitos de regra de negocio**. Sua missao: **encontrar, corrigir e provar**.

O enunciado completo (especificacao das regras, fases, entregaveis e rubrica) esta no PDF
que acompanha este projeto. **A especificacao (secao 4 do enunciado) e a fonte da verdade** -
nao o codigo.

## Requisitos

- .NET SDK 8.0 (`dotnet --version`)
- Git
- Acesso a internet na primeira execucao (restauracao de pacotes NuGet)

## Comandos essenciais

```bash
dotnet build                                   # compila tudo
dotnet run --project src/Triangulos.Api --urls http://localhost:5000   # sobe a API
dotnet test                                    # roda a suite de testes
./scripts/gerar-evidencias.sh antes            # gera evidencias (Windows: .\scripts\gerar-evidencias.ps1 antes)
```

Chamadas de exemplo: arquivo `requests.http`.

## Estrutura

```
src/Triangulos.Core/     regras de negocio (biblioteca)
src/Triangulos.Api/      Minimal API que expoe o Core
tests/Triangulos.Tests/  SEU projeto de testes (xUnit) - so tem exemplos
evidencias/              onde voce guarda a prova do seu trabalho
RELATORIO-BUGS.md        modelo do relatorio a preencher
```

## Regras do jogo

1. **Fase 1 (caixa-preta):** NAO abra `src/Triangulos.Core`. Explore pela API e compare com a especificacao.
2. Escreva os testes **antes** de corrigir. Rode-os contra o codigo original e guarde a evidencia "antes".
3. So depois abra o Core, corrija um bug por vez e faca um commit por bug.
4. **Nunca** apague, enfraqueca ou pule (`Skip`) um teste para fazer a suite passar.
