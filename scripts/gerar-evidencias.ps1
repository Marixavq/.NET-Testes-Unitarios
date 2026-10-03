# Uso:  .\scripts\gerar-evidencias.ps1 antes     (ou "depois")
param([string]$Rotulo = "ultimo")

$Destino = "evidencias/$Rotulo"
if (Test-Path $Destino) { Remove-Item -Recurse -Force $Destino }
New-Item -ItemType Directory -Force -Path $Destino | Out-Null

dotnet test `
  --logger "trx;LogFileName=resultado.trx" `
  --logger "console;verbosity=normal" `
  --collect:"XPlat Code Coverage" `
  --results-directory $Destino 2>&1 | Tee-Object -FilePath "$Destino/console.txt"

Write-Host "`nEvidencias salvas em $Destino/"
