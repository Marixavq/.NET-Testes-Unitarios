#!/usr/bin/env bash
# Uso:  ./scripts/gerar-evidencias.sh antes      (ou "depois")
# Gera evidencias/<rotulo>/ com: relatorio .trx, saida do console e cobertura (Cobertura XML).
set -euo pipefail

ROTULO="${1:-ultimo}"
DESTINO="evidencias/${ROTULO}"

rm -rf "${DESTINO}"
mkdir -p "${DESTINO}"

# "|| true": queremos registrar a evidencia mesmo quando ha testes falhando (fase "antes").
dotnet test \
  --logger "trx;LogFileName=resultado.trx" \
  --logger "console;verbosity=normal" \
  --collect:"XPlat Code Coverage" \
  --results-directory "${DESTINO}" 2>&1 | tee "${DESTINO}/console.txt" || true

echo
echo "Evidencias salvas em ${DESTINO}/"
