# Pasta de evidencias

Coloque aqui a prova do seu trabalho:

```
evidencias/
  antes/     <- execucao dos SEUS testes contra o codigo ORIGINAL (deve conter falhas)
  depois/    <- execucao dos SEUS testes contra o codigo CORRIGIDO (tudo verde + cobertura)
  sensibilidade/  <- (opcional, recomendado) prints/saidas mostrando que, ao reintroduzir um bug, o teste volta a falhar
```

Gere com:  `./scripts/gerar-evidencias.sh antes`  e  `./scripts/gerar-evidencias.sh depois`
(Windows: `.\scripts\gerar-evidencias.ps1 antes`).
