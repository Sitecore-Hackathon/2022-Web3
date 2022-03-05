# setup
$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

# start
$operatorPath = Join-Path $PSScriptRoot "\operator"

Push-Location -Path $operatorPath

try
{
    docker-compose down
    docker-compose up -d

    $LASTEXITCODE -ne 0 | Where-Object { $_ } | ForEach-Object { throw "Start failed." }
}
finally
{
    Pop-Location
}

